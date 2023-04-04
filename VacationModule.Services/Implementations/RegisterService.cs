using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DataAccess;
using VacationModule.Services.Interfaces;

namespace VacationModule.Services.Implementations
{
    public class RegisterService: IRegisterService
    {
        private readonly VacationModuleContext _dbContext;

        public RegisterService(VacationModuleContext dbContext) { _dbContext = dbContext; }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool IsAlreadyRegistered(string email)
        {
            var response = _dbContext.Users.Where(user => user.Email == email).FirstOrDefault();
            if (response != null)
            {
                return true;
            }
            return false;
        }
    }
}
