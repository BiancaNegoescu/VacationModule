using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationModule.DTO;
using VacationModule.POCO;
using VacationModule.Services.Constants;
using VacationModule.Services.Interfaces;

namespace VacationModule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : Controller
    {

        private readonly IQueryService _queryService;
        private readonly IVacationRequestService _vacationRequestService;

        public HolidayController(IQueryService queryService, IVacationRequestService vacationRequestService)
        {
            _queryService = queryService;
            _vacationRequestService = vacationRequestService;
        }

        [HttpGet("getHolidays"), Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> getHolidays()
        {
            int year = Year.CurrentYear;
            try
            {
                List<NationalHolidayDTO> nationalHolidays = await _queryService.nationalHolidays(year);
                return Ok(nationalHolidays);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("holidayList"), Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> getHolidayList()
        {
            int year = Year.CurrentYear;
            try
            {
                List<DateTime> list = await _queryService.holidayList(year);
                return Ok(list);
            } catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost("requestHoliday"), Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> requestHoliday(FormVacationRequestDTO request)
        {
            VacationRequestDTO myRequest;
            try
            {
                myRequest = await _vacationRequestService.makeVacationRequest(request);
            } catch(ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return Ok(myRequest);
        }

        [HttpGet("myVacationRequests"), Authorize(Roles = "admin,employee")]
        public ActionResult myVacationRequests()
        {
            try
            {
                List<VacationRequestDTO> myRequests = _vacationRequestService.getMyRequests();
                return Ok(myRequests);
            } catch(ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("modifyRequest"), Authorize(Roles = "admin,employee")]
        public async Task<ActionResult> modifyRequest(ModifyRequestDTO request)
        {
            try
            {
                VacationRequestDTO newRequest = await _vacationRequestService.modifyRequest(request);
                return Ok(newRequest);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("allRequests"), Authorize(Roles = "admin")]
        public ActionResult allRequests()
        {
            List<AdminRequestsDTO> requests = _vacationRequestService.getAllRequests();
            return Ok(requests);
        }

        [HttpGet("myAvailableDays"), Authorize(Roles = "admin,employee")]
        public ActionResult getAvailableDays(int year)
        {
            int availableDays;
            try
            {
                if (year != Year.CurrentYear)
                {
                    availableDays = _vacationRequestService.getAvailableDaysNextYear(year);
                }
                else
                {
                    availableDays = _vacationRequestService.getAvailableDays(year);
                }
                return Ok(availableDays);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
