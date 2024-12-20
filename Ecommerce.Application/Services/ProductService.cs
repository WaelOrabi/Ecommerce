using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService(IUnitOfWork unitOfWork):IProductService
    {
        public async Task<Product> Add(ProductRequest productRequset)
        {
            if (productRequset == null)
            {
                throw new ArgumentNullException();
            }
            using var stream = new MemoryStream();
            await productRequset.Image.CopyToAsync(stream);
            var product = new Product
            {
                Name = productRequset.Name,
                Quantity = productRequset.Quantity,
                Description = productRequset.Description,
                Price = productRequset.Price,
                CreateAt = productRequset.CreateAt,
                Image = stream.ToArray(),
                CategoryId=productRequset.CategoryId,

            };
            var result=await unitOfWork.ProductRepository.AddAsync(product);
            await unitOfWork.CompleteAsync();
            return result;
        }
        public async Task<Product> Update(ProductRequest productRequset)
        {

            if (productRequset == null)
            {
                throw new ArgumentNullException();
            }
            using var stream = new MemoryStream();
            await productRequset.Image.CopyToAsync(stream);
            var product = new Product
            {
                Name = productRequset.Name,
                Quantity = productRequset.Quantity,
                Description = productRequset.Description,
                Price = productRequset.Price,
                CreateAt = productRequset.CreateAt,
                Image = stream.ToArray(),
                CategoryId = productRequset.CategoryId,

            };
            var result =  unitOfWork.ProductRepository.Update(product);
            await unitOfWork.CompleteAsync();
            return result;
        }
        public async Task<Product> GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentNullException();
            }
            var result=await unitOfWork.ProductRepository.GetByIdAsync(id);
            return result;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var result=await unitOfWork.ProductRepository.GetAllAsync();
            return result;
        }
        public async Task<int> Delete(int id)
        {
            await unitOfWork.ProductRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

     
    }
}
