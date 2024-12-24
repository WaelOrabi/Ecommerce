using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAddressService
    {
        Task<Address>GetById(int id);
        Task<IEnumerable<Address>> GetAll();    
        Task<Address>Add(AddressRequest addressRequest);
        Task<Address>Update(AddressRequest addressRequest);
        Task<int>Delete(int id);
    }
}
