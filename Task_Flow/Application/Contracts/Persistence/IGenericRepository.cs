using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> ListAllAsync();
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
