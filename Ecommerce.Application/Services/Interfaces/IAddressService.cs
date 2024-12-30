using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services.Interfaces
{
    public interface IAddressService
    {
        Task<AddressResponse> GetById(int id);
        Task<IEnumerable<AddressResponse>> GetAll();
        Task<AddressResponse> Add(AddressRequestDTO addressRequest);
        Task<AddressResponse?> Update(AddressRequestDTO addressRequest, int id);
        Task<int> Delete(int id);
    }
}
