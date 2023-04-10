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
        public ActionResult Register(RegisterDTO request)
        {
            try
            {
                _userService.Register(request);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDTO request) 
        {
            try
            {
                string token = _userService.Login(request);
                if (token != null)
                {
                    return Ok(token);
                }
                return BadRequest();
            } catch(ArgumentException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
