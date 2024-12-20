using Application.Interfaces;
using Application.Repositories;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Repositories;

namespace Ecommerce.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
            AccountRepository = new AccountRepository(_dbContext);
            AddressRepository = new AddressRepository(_dbContext);
            CategoryRepository = new CategoryRepository(_dbContext);
            OrderRepository =new OrderRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
            ReviewRepository = new ReviewRepository(_dbContext);
            RoleRepository=new RoleRepository(_dbContext);

            
        }
        public IAccountRepository AccountRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public IReviewRepository ReviewRepository { get; private set; }

        public IRoleRepository RoleRepository { get; private set; }

        public IAddressRepository AddressRepository { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

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
