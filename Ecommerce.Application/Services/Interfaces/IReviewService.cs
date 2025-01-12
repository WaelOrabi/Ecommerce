using Ecommerce.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IReviewService
    {
        Task<Review> GetById(int id);
        Task<IEnumerable<Review>> GetAll();
        Task<Review> Add();
        Task<Review> Update();
        Task<int> Delete(int id);
    }
}
