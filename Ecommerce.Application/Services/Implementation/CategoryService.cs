using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Category;
using Ecommerce.Application.Resources;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.Services.Implementation
{
    public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer) : ResponseHandler(localizer), ICategoryService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<SharedResources> _localizer = localizer;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Add(CategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
                return GenerateNotFoundResponse<string>();
            Category category = _mapper.Map<Category>(categoryRequest);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("");
        }

        public async Task<Response<int>> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return GenerateNotFoundResponse<int>();

            await _unitOfWork.CategoryRepository.DeleteByIdAsync(category);
            await _unitOfWork.CompleteAsync();
            return GenerateDeleteResponse<int>();
        }

        public async Task<Response<IEnumerable<CategoryResponse>>> GetAll()
        {
            var result = await _unitOfWork.CategoryRepository.GetAllAsync();
            var resultMapping = _mapper.Map<IEnumerable<CategoryResponse>>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<Response<CategoryResponse>> GetById(int id)
        {

            var result = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (result == null)
                return GenerateNotFoundResponse<CategoryResponse>();
            var resultMapping = _mapper.Map<CategoryResponse>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<bool> IsCategoryExist(int id)
        {
            var result = await _unitOfWork.CategoryRepository.GetTableNoTracking().AnyAsync(x => x.Id.Equals(id));
            return result;
        }

        public async Task<Response<string>> Update(CategoryRequest categoryRequest, int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return GenerateNotFoundResponse<string>();
            category = _mapper.Map(categoryRequest, category);
            unitOfWork.CategoryRepository.Update(category);
            await unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("");
        }
    }
}
