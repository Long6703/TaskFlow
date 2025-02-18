using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext
{
    public class TaskFlowContext : DbContext
    {
        public TaskFlowContext(DbContextOptions<TaskFlowContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
