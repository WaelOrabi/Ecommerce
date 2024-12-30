using Application.Interfaces;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart> GetByIdIncludeCartItemAsync(int id);
    }
}
