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
        public Task makeVacationRequest(FormVacationRequestDTO request);
        public Task<List<DateTime>> getOnlyWorkingDays(List<DateTime> requestedDates);

        public Task<List<DateTime>> getDaysWithoutHolidays(FormVacationRequestDTO request);

        public List<VacationRequestDTO> getMyRequests();
    }
}
