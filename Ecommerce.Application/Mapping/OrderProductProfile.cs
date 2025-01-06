using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.OrderProduct;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProduct, OrderProductResponse>();
            CreateMap<OrderProductRequest, OrderProduct>();
        }
    }
}
