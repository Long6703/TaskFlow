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
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskFlowContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
