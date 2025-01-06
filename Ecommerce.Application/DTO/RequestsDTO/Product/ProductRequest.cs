using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ecommerce.Application.DTO.RequestsDTO.Product
{
    public class ProductRequest
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }

        public DateTime CreateAt { get; set; }



        [FromForm]
        [NotMapped]
        public List<IFormFile> Files { get; set; }

        [FromForm]
        [NotMapped]
        public List<string>? FileDescriptions { get; set; }
        public int CategoryId { get; set; }
    }
}
