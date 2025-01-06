using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Role;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleResponse>();

            CreateMap<RoleRequest, Role>();
        }
    }
}
