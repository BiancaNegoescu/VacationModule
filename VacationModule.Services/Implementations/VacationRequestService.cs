﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DataAccess;
using VacationModule.DTO;
using VacationModule.POCO;
using VacationModule.Services.Constants;
using VacationModule.Services.Interfaces;

namespace VacationModule.Services.Implementations
{
    public class VacationRequestService: IVacationRequestService
    {

        private readonly IUserService _userService;
        private readonly IQueryService _queryService;
        private readonly IMapper _mapper;
        private readonly VacationModuleContext _dbContext;

        public VacationRequestService(IUserService userService, IMapper mapper, VacationModuleContext dbContext, IQueryService queryService)
        {
            _userService = userService;
            _mapper = mapper;
            _dbContext = dbContext;
            _queryService = queryService;
        }

        public async Task<VacationRequestDTO> makeVacationRequest(FormVacationRequestDTO request)
        {
            /*
            VacationRequest vacationRequest = new VacationRequest();
            vacationRequest.requestedDays = new List<DateTime>();
            Guid userId = _userService.GetMe();
            vacationRequest.UserId = userId;
            */

            int startDay = request.startDay;
            int startMonth = request.startMonth;
            int startYear = request.startYear;
            int endDay = request.endDay;
            int endMonth = request.endMonth;
            int endYear = request.endYear;

            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);

            List<DateTime> requestedDays = new List<DateTime>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                requestedDays.Add(date);
            }

            requestedDays = await getOnlyWorkingDays(requestedDays);

            int availableDays = getAvailableDays();

            bool eligible = eligibleForRequest(availableDays, requestedDays.Count);
            if (eligible)
            {
                VacationRequest vacationRequest = new VacationRequest();
                vacationRequest.requestedDays = new List<DateTime>();
                vacationRequest.requestedDays = requestedDays;
                Guid userId = _userService.GetMe();
                vacationRequest.UserId = userId;

                _dbContext.VacationRequests.Add(vacationRequest);
                _dbContext.SaveChanges();

                VacationRequestDTO newRequest = _mapper.Map<VacationRequest, VacationRequestDTO>(vacationRequest);

                return newRequest;
            } else
            {
                throw new ArgumentException("You do not have left as many days as you requested!");
            }
        }

       
        public async Task<List<DateTime>> getOnlyWorkingDays(List<DateTime> requestedDates)
        {

            List<DateTime> holidaysList = await _queryService.holidayList();
            if (holidaysList != null)
            {
                for (int i = 0; i < holidaysList.Count; i++)
                {
                    requestedDates.Remove(holidaysList[i]);
                }
            }
            for(int i = 0; i < requestedDates.Count; i++)
            {
                if (WeekendDays.weekend.Contains(requestedDates[i].ToString("dddd")))
                {
                    requestedDates.Remove(requestedDates[i]);
                    i--;
                }
            }
            return requestedDates;

        }

        public List<VacationRequestDTO> getMyRequests()
        {
            Guid myId = _userService.GetMe();
            List<VacationRequest> myRequests = _dbContext.VacationRequests.Where(x => x.UserId == myId).ToList();
            List<VacationRequestDTO> requestsDTO = new();

            if (myRequests.Count == 0)
            {
                throw new Exception("You do not have any vacation requests!");
            }

            for (int i = 0; i < myRequests.Count; i++)
            {
                var request = _mapper.Map<VacationRequest, VacationRequestDTO>(myRequests[i]);
                requestsDTO.Add(request);
            }

            return requestsDTO;
        }

        public async Task<VacationRequestDTO> modifyRequest(ModifyRequestDTO request)
        {
            Guid myId = _userService.GetMe();

            VacationRequest? vacationRequest = _dbContext.VacationRequests.FirstOrDefault(x => x.Id == request.Id);
            if (vacationRequest == null)
            {
                throw new ArgumentException("There does not exist a request with introduced ID!");
            }
            if (vacationRequest.UserId != myId)
            {
                throw new ArgumentException("You do not have any request with introduced ID!");
            }
            
            int startDay = request.startDay;
            int startMonth = request.startMonth;
            int startYear = request.startYear;
            int endDay = request.endDay;
            int endMonth = request.endMonth;
            int endYear = request.endYear;

            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);

            List<DateTime> requestedDays = new List<DateTime>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                requestedDays.Add(date);
            }

            requestedDays = await getOnlyWorkingDays(requestedDays);


            bool eligible = eligibleForModifying(vacationRequest, requestedDays.Count);
            if(eligible)
            {
                // delete the days from old request
                vacationRequest.requestedDays.Clear();
                vacationRequest.requestedDays = requestedDays;

                _dbContext.SaveChanges();

                VacationRequestDTO newRequest = _mapper.Map<VacationRequest, VacationRequestDTO>(vacationRequest);

                return newRequest;

            } else
            {
                throw new ArgumentException("You do not have left as many days as you requested!");
            }
        }

        public List<AdminRequestsDTO> getAllRequests()
        {
            List<VacationRequest> requests = _dbContext.VacationRequests.ToList();
            List<AdminRequestsDTO> requestDTOs = new();
            for(int i = 0; i < requests.Count; i++)
            {
                AdminRequestsDTO requestDTO = _mapper.Map<VacationRequest, AdminRequestsDTO>(requests[i]);
                requestDTOs.Add(requestDTO);

            }
            return requestDTOs;
        }

        public int getAvailableDays()
        {
            List<VacationRequestDTO> requests = getMyRequests();
            int unavailableDays = requests.Aggregate(0, (acc, req) => acc + req.requestedDays.Count);
            int remainedDays = AvailableDaysPerYear.Amount - unavailableDays;
            return remainedDays;
        }

        public bool eligibleForRequest(int remaindedDays, int nrOfDaysRequested)
        {
            if (remaindedDays < nrOfDaysRequested)
            {
                return false;
            }
            return true;
        }

        public bool eligibleForModifying(VacationRequest vacationRequest, int nrOfDaysRequested)
        {
            int remaindedDays = getAvailableDays();
            int daysInRequest = vacationRequest.requestedDays.Count;
            int totalAvailableDays = remaindedDays + daysInRequest;
            if (nrOfDaysRequested > totalAvailableDays)
            {
                return false;
            }
            return true;

        }
    }
}
