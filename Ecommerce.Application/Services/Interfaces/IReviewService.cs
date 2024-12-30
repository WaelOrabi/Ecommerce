using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
