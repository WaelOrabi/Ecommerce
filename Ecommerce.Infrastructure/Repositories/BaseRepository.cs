using Application.Interfaces;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(ApplicationDbContext applicationDbContext) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext appDbContext = applicationDbContext;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await appDbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            TEntity result = await appDbContext.Set<TEntity>().FindAsync(id);
            var result2 = appDbContext.Set<TEntity>().Remove(result);

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await appDbContext.Set<TEntity>().ToListAsync<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await appDbContext.Set<TEntity>().FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            appDbContext.Set<TEntity>().Update(entity);
            return entity;
        }
    }
}
