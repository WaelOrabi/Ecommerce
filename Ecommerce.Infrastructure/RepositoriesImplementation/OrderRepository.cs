using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
using Ecommerce.Infrastructure.RepositoriesImplementation;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
    }
}
