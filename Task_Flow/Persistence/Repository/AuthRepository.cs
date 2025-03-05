using Application.Contracts.IRepository;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TaskFlowContext _taskFlowContext;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(TaskFlowContext taskFlowContext, ILogger<AuthRepository> logger)
        {
            _taskFlowContext = taskFlowContext;
            _logger = logger;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            _logger.LogInformation($"EmailExistsAsync called for {email}");
            var user = await _taskFlowContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user != null;
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
