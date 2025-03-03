using Application.Contracts.Email;
using Application.Models.EmailModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IEmailService _emailService;

        public TestController(ILogger<TestController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
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

        [HttpGet("send")]
        public async Task<IActionResult> SendEmail()
        {
            _logger.LogInformation("Sending email...");
            await _emailService.QueueEmailAsync(
                "nguyenkhaclong12a8pkk@gmail.com",
                "Thông báo mới",
                "<p>Đây là nội dung <strong>HTML</strong> của email</p>",
                true);

            return Ok("Email sent successfully.");
        }
    }
}
