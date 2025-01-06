using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class CategoryRepository(ApplicationDbContext context) : BaseRepository<Category>(context), ICategoryRepository
    {
    }
}
