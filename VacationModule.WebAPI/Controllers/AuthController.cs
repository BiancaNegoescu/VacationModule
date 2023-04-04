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
        private readonly IConfiguration _configuration;
        private readonly VacationModuleContext _dbContext;
        private readonly IUserService _userService;
        private readonly IQueryService _queryService;

        public AuthController(IConfiguration configuration, VacationModuleContext dbContext, IUserService userService, IQueryService queryService)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _userService = userService;
            _queryService = queryService;
  
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

        [HttpGet("getHolidays")]
        public async Task<ActionResult> getHolidaysAsync()
        {
            List<Holiday> data =await _queryService.nationalHolidays("RO", 2023);
            return Ok(data);
        }

    }
}
