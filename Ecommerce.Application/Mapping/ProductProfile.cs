using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Product;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Domain.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>();

            CreateMap<ProductRequest, Product>();
            //  .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.Image)));
        }

        private byte[] ConvertIFormFileToByteArray(IFormFile formFile)
        {
            if (formFile == null)
            {
                return null;
            }

            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
