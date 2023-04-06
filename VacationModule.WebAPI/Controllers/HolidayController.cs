using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationModule.DTO;
using VacationModule.POCO;
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

        [HttpGet("getHolidays"), Authorize]
        public async Task<ActionResult> getHolidays ()
        {
            List<NationalHolidayDTO> nationalHolidays = await _queryService.nationalHolidays("RO", 2023);
            return Ok(nationalHolidays);
        }

        [HttpGet("holidayList")]
        public async Task<ActionResult> getHolidayList()
        {
            List<DateTime> list = await _queryService.holidayList();
            return Ok(list);
        }

        [HttpPost("requestHoliday"), Authorize]
        public async Task<ActionResult> requestHolidayAsync(FormVacationRequestDTO request)
        {
            await _vacationRequestService.makeVacationRequest(request);
            return Ok();
        }

        [HttpGet("myVacationRequests"), Authorize]
        public ActionResult myVacationRequests()
        {
            List<VacationRequestDTO> myRequests = _vacationRequestService.getMyRequests();
            return Ok(myRequests);
        }

        // FOR TESTING
        [HttpPost("getRequestWithoutHolidays"), Authorize]
        public async Task<ActionResult> getRequestWithoutHolidays(FormVacationRequestDTO request)
        {
            List<DateTime> list = await _vacationRequestService.getDaysWithoutHolidays(request);
            return Ok(list);
        }
    }
}
