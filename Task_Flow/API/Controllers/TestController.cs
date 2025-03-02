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
        private readonly IEmailSender _emailSender;

        public TestController(ILogger<TestController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
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
            EmailMessage email = new EmailMessage
            {
                To = "nguyenkhaclong12a8pkk@gmail.com",
                Subject = "Test Email",
                Body = "This is a test email."
            };

            var result = await _emailSender.SendEmailAsync(email);
            _logger.LogInformation("Email sent.");

            if (result)
            {
                return Ok("Email sent successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while sending the email.");
            }        
        }
    }
}
