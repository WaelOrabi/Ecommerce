using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Address;
using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Application.Services.Interfaces
{
    public interface IAddressService
    {
        Task<Response<AddressResponse>> GetById(int id);
        Task<Response<IEnumerable<AddressResponse>>> GetAll();
        Task<Response<string>> Add(AddressRequest addressRequest);
        Task<bool>IsAddressExist(int id);
        Task<Response<string>> Update(AddressRequest addressRequest, int id);
        Task<Response<int>> Delete(int id);
    }
}
