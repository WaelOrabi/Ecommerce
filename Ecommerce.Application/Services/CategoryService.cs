using Application.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services
{
    public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryResponse> Add(CategoryRequestDTO categoryRequest)
        {
            if (categoryRequest == null)
                throw new ArgumentNullException();
            Category category = _mapper.Map<Category>(categoryRequest);
            var result = await unitOfWork.CategoryRepository.AddAsync(category);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task<int> Delete(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();

            await unitOfWork.CategoryRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAll()
        {
            var result = await unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(result);
        }

        public async Task<CategoryResponse> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();
            var result = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task<CategoryResponse?> Update(CategoryRequestDTO categoryRequest, int id)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return null;
            category = _mapper.Map<Category>(categoryRequest);
            var result = unitOfWork.CategoryRepository.Update(category);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<CategoryResponse>(result); ;
        }
    }
}
