using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Order;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();
        }
    }
}
