using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("log-info")]
        public IActionResult LogInformation()
        {
            _logger.LogInformation("This is an Information log.");
            return Ok("Information log recorded.");
        }

        [HttpGet("log-warning")]
        public IActionResult LogWarning()
        {
            _logger.LogWarning("This is a Warning log.");
            return Ok("Warning log recorded.");
        }

        [HttpGet("log-error")]
        public IActionResult LogError()
        {
            _logger.LogError("This is an Error log.");
            return Ok("Error log recorded.");
        }
    }
}
