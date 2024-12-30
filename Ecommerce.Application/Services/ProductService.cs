using Application.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        private readonly IMapper _mapper = mapper;

        public async Task<ProductResponse> Add(ProductRequestDTO productRequset)
        {
            if (productRequset == null)
            {
                throw new ArgumentNullException();
            }
            using var stream = new MemoryStream();
            await productRequset.Image.CopyToAsync(stream);

            Product product = _mapper.Map<Product>(productRequset);
            var result = await unitOfWork.ProductRepository.AddAsync(product);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<ProductResponse>(result);
        }
        public async Task<ProductResponse?> Update(ProductRequestDTO productRequset, int id)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(id);
            if (productRequset == null)
            {
                return null;
            }
            using var stream = new MemoryStream();
            await productRequset.Image.CopyToAsync(stream);

            product = _mapper.Map<Product>(productRequset);

            var result = unitOfWork.ProductRepository.Update(product);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<ProductResponse>(result);
        }
        public async Task<ProductResponse> GetById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentNullException();
            }
            var result = await unitOfWork.ProductRepository.GetByIdAsync(id);
            return _mapper.Map<ProductResponse>(result);
        }
        public async Task<IEnumerable<ProductResponse>> GetAll()
        {
            var result = await unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(result);
        }
        public async Task<int> Delete(int id)
        {
            await unitOfWork.ProductRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }


    }
}
