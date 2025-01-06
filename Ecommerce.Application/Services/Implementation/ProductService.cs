using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Image;
using Ecommerce.Application.DTO.RequestsDTO.Product;
using Ecommerce.Application.Resources;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;


namespace Ecommerce.Application.Services.Implementation
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : ResponseHandler(localizer), IProductService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<SharedResources> _localizer = localizer;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public async Task<Response<string>> Add(ProductRequest productRequest)
        {
            var images = new List<ImageRequest>();
            if (productRequest.Files != null)
            {
                for (int i = 0; i < productRequest.Files.Count; i++)
                {
                    images.Add(new ImageRequest
                    {
                        File = productRequest.Files[i],
                        FileDescription = productRequest.FileDescriptions != null && productRequest.FileDescriptions.Count > i
                            ? productRequest.FileDescriptions[i]
                            : null
                    });
                }
            }

            var imagesMapping = _mapper.Map<IEnumerable<Image>>(images);

            var product = _mapper.Map<Product>(productRequest);

            product.Images = imagesMapping;
            if (product.Images != null && product.Images.Any())
            {
                foreach (var image in product.Images)
                {


                    var folderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Products");


                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }


                    var filePath = Path.Combine(folderPath, $"{image.FileName}{image.FileExtension}");
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.File.CopyToAsync(stream);
                        image.File = null;
                    }


                    image.FilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/Products/{image.FileName}{image.FileExtension}";
                }
            }

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();


            string key = $"Product-{product.Id}";
            var expiryTime = DateTimeOffset.UtcNow.AddMinutes(5);


            await _unitOfWork.CacheRepositroy.SetDataAsync(key, product, expiryTime);

            return GenerateSuccessResponse("Product added successfully.");

        }

        public async Task<Response<string>> Update(ProductRequest productRequest, int id)
        {

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return GenerateNotFoundResponse<string>();

            _mapper.Map(productRequest, product);
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CompleteAsync();


            string productKey = $"Product-{id}";
            var expiryTime = DateTimeOffset.UtcNow.AddMinutes(5);
            await _unitOfWork.CacheRepositroy.SetDataAsync(productKey, product, expiryTime);

            return GenerateSuccessResponse("Product updated successfully.");

        }


        public async Task<Response<ProductResponse>> GetById(int id)
        {

            string key = $"Product-{id}";

            var cacheData = await _unitOfWork.CacheRepositroy.GetDataAsync<Product>(key);

            if (cacheData != null)
            {
                var cachedProductResponse = _mapper.Map<ProductResponse>(cacheData);
                return GenerateSuccessResponse(cachedProductResponse);
            }


            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return GenerateNotFoundResponse<ProductResponse>();


            var expiryTime = DateTimeOffset.UtcNow.AddMinutes(5);
            await _unitOfWork.CacheRepositroy.SetDataAsync(key, product, expiryTime);

            var resultMapping = _mapper.Map<ProductResponse>(product);
            return GenerateSuccessResponse(resultMapping);


        }


        public async Task<Response<IEnumerable<ProductResponse>>> GetAll()
        {

            string key = "AllProducts";


            var cacheData = await _unitOfWork.CacheRepositroy.GetDataAsync<IEnumerable<Product>>(key);
            if (cacheData != null)
            {
                var cachedProductResponse = _mapper.Map<IEnumerable<ProductResponse>>(cacheData);
                return GenerateSuccessResponse(cachedProductResponse);
            }

            var result = await _unitOfWork.ProductRepository.GetAllAsync();
            if (result == null)
                return GenerateNotFoundResponse<IEnumerable<ProductResponse>>();

            var expiryTime = DateTimeOffset.UtcNow.AddMinutes(5);
            await _unitOfWork.CacheRepositroy.SetDataAsync(key, result, expiryTime);

            var resultMapping = _mapper.Map<IEnumerable<ProductResponse>>(result);
            return GenerateSuccessResponse(resultMapping);


        }

        public async Task<Response<int>> Delete(int id)
        {
            string key = $"Product-{id}";

            await _unitOfWork.CacheRepositroy.RemoveDataAsync(key);

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return GenerateNotFoundResponse<int>();

            await unitOfWork.ProductRepository.DeleteByIdAsync(product);
            await unitOfWork.CompleteAsync();
            return GenerateSuccessResponse(id);
        }

        public async Task<bool> IsProductExist(int id)
        {

            var result = await _unitOfWork.ProductRepository.GetTableNoTracking().AnyAsync(x => x.Id.Equals(id));
            return result;

        }
    }
}
