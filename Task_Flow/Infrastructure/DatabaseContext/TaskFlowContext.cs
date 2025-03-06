using Domain.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext
{
    public class TaskFlowContext : DbContext
    {
        public TaskFlowContext(DbContextOptions<TaskFlowContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskFlowContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseAuditEntity<int> || e.Entity is BaseAuditEntity<Guid>)
                .ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity as dynamic;

                if (entry.State == EntityState.Added)
                {
                    entity.DateCreated = DateTime.UtcNow;
                    entity.CreatedBy = "System";
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.DateModified = DateTime.UtcNow;
                    entity.ModifiedBy = "System";
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
