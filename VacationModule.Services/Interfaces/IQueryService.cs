using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.POCO;

namespace VacationModule.Services.Interfaces
{
    public interface IQueryService
    {
        public Task<List<Holiday>> nationalHolidays(string country, int year);
    }
}
