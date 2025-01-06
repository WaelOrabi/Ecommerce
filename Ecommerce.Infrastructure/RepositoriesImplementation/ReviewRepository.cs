using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    internal class ReviewRepository(ApplicationDbContext context) : BaseRepository<Review>(context), IReviewRepository
    {
    }
}
