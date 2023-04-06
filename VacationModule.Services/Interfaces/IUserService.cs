using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DTO;

namespace VacationModule.Services.Interfaces
{
    public interface IUserService
    {
        public string Login(LoginDTO request);

        public void Register(RegisterDTO request);


        public Guid GetMe();

    }
}
