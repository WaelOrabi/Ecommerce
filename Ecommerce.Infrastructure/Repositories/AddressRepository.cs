using Application.Repositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
namespace Ecommerce.Infrastructure.Repositories
{
    public class AddressRepository(ApplicationDbContext context):BaseRepository<Address>(context), IAddressRepository
    {
    }
}
