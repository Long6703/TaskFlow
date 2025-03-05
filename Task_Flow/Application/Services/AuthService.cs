using Application.Contracts.IRepository;
using Application.Exceptions;
using Application.IService;
using Application.Validation;
using Microsoft.Extensions.Logging;
using Shared.Models;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
        }

        public Task<string> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            _logger.LogInformation($"RegisterAsync called for {registerRequest.Email}");
            var validator = new RegisterValidator(_authRepository);
            var validationResult = await validator.ValidateAsync(registerRequest);
            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Register Request Request", validationResult);
            }
            return await _authRepository.EmailExistsAsync(registerRequest.Email);
        }
    }
}
