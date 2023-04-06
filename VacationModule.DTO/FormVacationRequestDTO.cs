using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationModule.DTO
{
    public class FormVacationRequestDTO
    {
       // public List<DateTime>? requestedDays { get; set; }
        public int startDay { get; set; }
        public int startMonth { get; set; }
        public int startYear { get; set; }

        public int endDay { get; set; }
        public int endMonth { get; set; }
        public int endYear { get; set; }

    }
}
