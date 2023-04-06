using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationModule.POCO
{
    public class NationalHoliday
    {
        public String country {  get; set; }
        public String iso { get; set; }
        public int year { get; set; }
        public String date { get; set; }
        public String day { get; set; }
        public String name { get; set; }
        public String type { get; set; }
    }
}
