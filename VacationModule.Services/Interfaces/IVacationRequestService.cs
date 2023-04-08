using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DTO;

namespace VacationModule.Services.Interfaces
{
    public interface IVacationRequestService
    {
        public Task<VacationRequestDTO> makeVacationRequest(FormVacationRequestDTO request);
        public Task<List<DateTime>> getOnlyWorkingDays(List<DateTime> requestedDates);

        public List<VacationRequestDTO> getMyRequests();

        public Task<VacationRequestDTO> modifyRequest(ModifyRequestDTO request);

        public List<AdminRequestsDTO> getAllRequests();
        public int getAvailableDays();

        public bool eligibleForRequest(int remaindedDays, int nrOfDaysRequested);




    }
}
