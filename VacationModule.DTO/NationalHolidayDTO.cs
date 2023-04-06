using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationModule.DTO
{
    public class NationalHolidayDTO
    {
        public String country { get; set; }
        public int year { get; set; }
        public DateTime date { get; set; }
        public String weekDay { get; set; }
        public String holidayName { get; set; }
        public String holidayType { get; set; }

    }
}
