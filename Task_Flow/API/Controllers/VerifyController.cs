using API.Helper;
using Application.Contracts.Email;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly ILogger<VerifyController> _logger;
        private readonly IEmailService _emailService;

        public VerifyController(ILogger<VerifyController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email)
        {
            _logger.LogInformation("Sending email to " + email);
            string verificationCode = GenerateVerificationCode.GenerateRandomCode();
            await _emailService.QueueEmailAsync(
            email,
            "TaskFlow - Verification Code",
            "<p>Hello,</p>" +
            "<p>You have requested a verification code for your TaskFlow account.</p>" +
            "<p><strong>Your verification code is: <span style='color:#2563eb; font-size:18px;'>" + verificationCode + "</span></strong></p>" +
            "<p>Please enter this code to complete the verification process.</p>" +
            "<p>If you did not request this, please ignore this email.</p>" +
            "<p>Best regards,<br>TaskFlow Team</p>",
            true);
            return Ok(verificationCode);
        }
    }
}
