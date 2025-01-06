using AutoMapper;
using Ecommerce.Application.DTO.RequestsDTO.Image;
using Ecommerce.Domain.Entities;

public class ImageProfile : Profile
{
    public ImageProfile()
    {

        CreateMap<ImageRequest, Image>()
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.File.FileName)))
            .ForMember(dest => dest.FileExtension, opt => opt.MapFrom(src => Path.GetExtension(src.File.FileName)))
            .ForMember(dest => dest.FileSizeInBytes, opt => opt.MapFrom(src => src.File.Length))
            .ForMember(dest => dest.FilePath, opt => opt.Ignore())
            .ForMember(dest => dest.FileDescription, opt => opt.MapFrom(src => src.FileDescription))
            .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.File))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());
    }
}
