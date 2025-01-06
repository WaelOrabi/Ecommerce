using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.DTO.RequestsDTO.Image
{
    public class ImageRequest
    {
        public IFormFile File { get; set; }
        public string? FileDescription { get; set; }
    }
}
