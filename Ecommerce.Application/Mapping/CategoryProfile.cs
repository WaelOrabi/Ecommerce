using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Category;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryRequest, Category>();
        }
    }
}
