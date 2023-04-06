using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DTO;
using VacationModule.POCO;

namespace VacationModule.Services.Interfaces
{
    public interface IQueryService
    {
        public Task<List<NationalHolidayDTO>> nationalHolidays(string country, int year);
        public DateTime stringToDate(string dateAsString);
        public Task<List<DateTime>> holidayList();
    }
}
