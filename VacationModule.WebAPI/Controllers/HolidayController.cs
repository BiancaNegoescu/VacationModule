﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("getHolidays"), Authorize]
        public async Task<ActionResult> getHolidays ()
        {
            int year = Year.CurrentYear;
            List<NationalHolidayDTO> nationalHolidays = await _queryService.nationalHolidays(year);
            return Ok(nationalHolidays);
        }

        [HttpGet("holidayList")]
        public async Task<ActionResult> getHolidayList()
        {
            int year = Year.CurrentYear;
            List<DateTime> list = await _queryService.holidayList(year);
            return Ok(list);
        }

        [HttpPost("requestHoliday"), Authorize]
        public async Task<ActionResult> requestHoliday(FormVacationRequestDTO request)
        {
            VacationRequestDTO myRequest = await _vacationRequestService.makeVacationRequest(request);
            return Ok(myRequest);
        }

        [HttpGet("myVacationRequests"), Authorize]
        public ActionResult myVacationRequests()
        {
            List<VacationRequestDTO> myRequests = _vacationRequestService.getMyRequests();
            return Ok(myRequests);
        }

        [HttpPut("modifyRequest"), Authorize]
        public async Task<ActionResult> modifyRequest(ModifyRequestDTO request)
        {
            VacationRequestDTO newRequest = await _vacationRequestService.modifyRequest(request);
            return Ok(newRequest);
        }

        [HttpGet("allRequests"), Authorize(Roles = "admin")]
        public ActionResult allRequests()
        {
            List<AdminRequestsDTO> requests = _vacationRequestService.getAllRequests();
            return Ok(requests);
        }

        [HttpGet("myAvailableDays"), Authorize]
        public ActionResult getAvailableDays(int year)
        {
            int availableDays = _vacationRequestService.getAvailableDays(year);
            return Ok(availableDays);
        }

    }
}
