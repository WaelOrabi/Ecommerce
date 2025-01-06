using Ecommerce.Domain.Entities;

namespace Ecommerce.Infrastructure.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart> GetByIdIncludeCartItemAsync(int id);
    }
}
