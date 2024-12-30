using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Domain.Mapping
{
    public class OrderProductProfile : Profile
    {
        public OrderProductProfile()
        {
            CreateMap<OrderProduct, OrderProductResponse>();
            CreateMap<OrderProductRequestDTO, OrderProduct>();
        }
    }
}
