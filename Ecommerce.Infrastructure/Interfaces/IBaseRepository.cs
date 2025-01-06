namespace Ecommerce.Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetTableNoTracking();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteByIdAsync(TEntity entity);
    }
}
