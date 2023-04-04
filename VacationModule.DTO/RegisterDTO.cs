using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationModule.DTO
{
    public class RegisterDTO
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
