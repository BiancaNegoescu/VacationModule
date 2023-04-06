using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationModule.POCO
{
    public class VacationRequest
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public List<DateTime> requestedDays { get; set; }


        
        public User User { get; set; }
    }
}
