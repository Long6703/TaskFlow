using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Application.IService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserCreateDTO userCreateDTO)
        {
            _logger.LogInformation("RegisterAsync is called");
            return Ok(await _userService.RegisterAsync(userCreateDTO));
        }
    }
}
