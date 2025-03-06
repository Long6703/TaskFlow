using Application.Contracts.IRepository;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(TaskFlowContext context, ILogger<UserRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            _logger.LogInformation($"EmailExistsAsync called for {email}");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user != null;
        }

        public Task<IEnumerable<string>> GetUserPermissions(Guid userId)
        {
            _logger.LogInformation("GetUserPermissions called.");
            throw new NotImplementedException();
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
