using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Address;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressResponse>();
            CreateMap<AddressRequest, Address>();
        }
    }
}
