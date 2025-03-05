using Application.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestFunctionController : ControllerBase
    {
        private readonly ILogger<TestFunctionController> _logger;
        private readonly IAuthService _authService;

        public TestFunctionController(ILogger<TestFunctionController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("checkEmailExist")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
        {
            _logger.LogInformation("RegisterAsync called.");
            var result = await _authService.RegisterAsync(registerRequest);
            if (result)
            {
                _logger.LogInformation($"Email {registerRequest.Email} exists.");
            }
            else
            {
                _logger.LogInformation($"Email {registerRequest.Email} does not exist.");
            }
            return Ok(result);
        }

    }
}
