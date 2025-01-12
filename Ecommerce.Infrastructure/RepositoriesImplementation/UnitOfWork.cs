using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
using Ecommerce.Infrastructure.Repositories;
using StackExchange.Redis;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public UnitOfWork(ApplicationDbContext dbContext, IConnectionMultiplexer connectionMultiplexer)
        {
            _dbContext = dbContext;
            _connectionMultiplexer = connectionMultiplexer;


            CategoryRepository = new CategoryRepository(_dbContext);
            OrderRepository = new OrderRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
            ReviewRepository = new ReviewRepository(_dbContext);

            CartRepository = new CartRepository(_dbContext);
            CartItemRepository = new CartItemRepository(_dbContext);
            UserRefreshTokenRepository = new UserRefreshTokenRepository(_dbContext);
            CacheRepositroy = new CacheRepository(_connectionMultiplexer);
        }



        public ICategoryRepository CategoryRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public IReviewRepository ReviewRepository { get; private set; }



        public ICartRepository CartRepository { get; private set; }

        public ICartItemRepository CartItemRepository { get; private set; }
        public ICacheRepositroy CacheRepositroy { get; private set; }

        public IUserRefreshTokenRepository UserRefreshTokenRepository { get; private set; }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
