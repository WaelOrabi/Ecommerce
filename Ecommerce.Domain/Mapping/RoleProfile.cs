using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Domain.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, Role>();
            CreateMap<RoleRequestDTO, Role>();
        }
    }
}
