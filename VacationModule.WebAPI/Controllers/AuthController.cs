using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationModule.DataAccess;
using VacationModule.DTO;
using VacationModule.POCO;
using VacationModule.Services.Interfaces;

namespace VacationModule.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
  
        }

        [HttpPost("register")]
        public ActionResult<User> Register(RegisterDTO request)
        {
            _userService.Register(request);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDTO request) 
        {
            string token = _userService.Login(request);
            if (token != null)
            {
                return Ok(token);
            }
            return BadRequest();

        }

    }
}
