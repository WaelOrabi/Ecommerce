using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Domain.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequestDTO, Order>();
        }
    }
}
