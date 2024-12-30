using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Domain.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartResponse>();
            CreateMap<CartRequestDTO, Cart>();
        }
    }
}
