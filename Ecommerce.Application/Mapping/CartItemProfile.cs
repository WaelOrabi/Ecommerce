using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.CartItem;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemResponse>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<CartItemRequest, CartItem>();
        }
    }
}
