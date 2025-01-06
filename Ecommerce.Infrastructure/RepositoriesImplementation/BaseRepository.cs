using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class BaseRepository<TEntity>(ApplicationDbContext applicationDbContext) : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext appDbContext = applicationDbContext;

        public async Task AddAsync(TEntity entity)
        {
            await appDbContext.Set<TEntity>().AddAsync(entity);

        }

        public async Task DeleteByIdAsync(TEntity entity)
        {

            appDbContext.Set<TEntity>().Remove(entity);

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await appDbContext.Set<TEntity>().ToListAsync<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await appDbContext.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetTableNoTracking()
        {
            return appDbContext.Set<TEntity>().AsNoTracking().AsQueryable();
        }

        public void Update(TEntity entity)
        {
            appDbContext.Set<TEntity>().Update(entity);

        }
    }
}
