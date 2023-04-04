using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.POCO;

namespace VacationModule.Services.Interfaces
{
    public interface ILoginService
    {
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        public string CreateToken(User user);
    }
}
