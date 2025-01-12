using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.EditUser;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.RegisterUser;
using Ecommerce.Application.DTO.ResponsesDTO;
using Ecommerce.Domain.DTO.Responses;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Identity;

namespace Ecommerce.Application.Mapping
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<UserRegisterRequest, User>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<EditUserRequest, User>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<User, UserResponse>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
           .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
           .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<Review, UserReviewsResponse>()
                 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                  .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                   .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));
            CreateMap<User, ListUserResponse>();
            CreateMap<User, ListUserPaginateResponse>();
        }
    }
}
