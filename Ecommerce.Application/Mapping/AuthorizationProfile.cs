using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.AddRoleRequest;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateRole;
using Ecommerce.Application.DTO.ResponsesDTO.Authorization;
using Ecommerce.Domain.Entities.Identity;

namespace Ecommerce.Application.Mapping
{
    public class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            CreateMap<AddRoleRequest, Role>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameRole));
            CreateMap<UpdateRoleRequest, Role>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<Role, RoleResponse>();
        }
    }
}
