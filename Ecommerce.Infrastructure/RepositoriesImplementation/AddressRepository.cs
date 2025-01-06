using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class AddressRepository(ApplicationDbContext context) : BaseRepository<Address>(context), IAddressRepository
    {
    }
}
