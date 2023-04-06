using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DataAccess;
using VacationModule.DTO;
using VacationModule.POCO;
using VacationModule.Services.Interfaces;

namespace VacationModule.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly VacationModuleContext _dbContext;
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public UserService(VacationModuleContext dbContext, ILoginService loginService, IRegisterService registerService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _loginService = loginService;
            _registerService = registerService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public string Login(LoginDTO request)
        {
            var response = _dbContext.Users.Where(user => user.Email == request.Email).FirstOrDefault();
            if (response == null)
            {
                throw new ArgumentException("User not found!");
            }

            if (!_loginService.VerifyPasswordHash(request.Password, response.PasswordHash, response.PasswordSalt))
            {
                throw new ArgumentException("Wrong password!");

            }

            string token = _loginService.CreateToken(response);
            return token;
        }

        public void Register(RegisterDTO request)
        {
            bool isAlreadyRegistered = _registerService.IsAlreadyRegistered(request.Email);
            if (isAlreadyRegistered)
            {
                throw new ArgumentException("There is a registered user with this email!");
            }
            _registerService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = _mapper.Map<RegisterDTO, User>(request);

            string fullNameUser = request.FirstName + " " + request.LastName;


            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

        }

        public Guid GetMe()
        {
            var result = String.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            }
            if (result == null)
            {
                throw new ArgumentException("Claim is null");
            }
            Guid guid = new Guid(result);

            return guid;
        }




    }
}
