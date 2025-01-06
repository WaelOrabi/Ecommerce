using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Cart;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartResponse>();
            CreateMap<CartRequest, Cart>();
        }
    }
}
