using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetById(int id);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> Add(OrderRequest orderRequest);
        Task<Order> Update(OrderRequest orderRequest);
        Task<int> Delete(int id);
    }
}
