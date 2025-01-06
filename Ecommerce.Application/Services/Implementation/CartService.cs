using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Cart;
using Ecommerce.Application.Resources;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.Services.Implementation
{
    public class CartService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer) : ResponseHandler(localizer), ICartService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Add(CartRequest cartRequest)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(cartRequest.AccountId);
            if (account == null)
            {

                return GenerateNotFoundResponse<string>();
            }
            var productIds = cartRequest.CartItems.Select(p => p.ProductId).ToList();
            var products = await _unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                return GenerateNotFoundResponse<string>("One or more products not found.");

            var cart = _mapper.Map<Cart>(cartRequest);
            await unitOfWork.CartRepository.AddAsync(cart);
            await unitOfWork.CompleteAsync();
            return GenerateCreatedResponse("");
        }

        public async Task<Response<int>> Delete(int id)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(id);
            if (cart == null)
                return GenerateNotFoundResponse<int>();
            await unitOfWork.CartRepository.DeleteByIdAsync(cart);
            await unitOfWork.CompleteAsync();
            return GenerateDeleteResponse(id);
        }

        public async Task<Response<IEnumerable<CartResponse>>> GetAll()
        {
            var result = await _unitOfWork.CartRepository.GetTableNoTracking().Include(x => x.CartItems).ThenInclude(x => x.Product).ToListAsync();
            var resultMapping = _mapper.Map<IEnumerable<CartResponse>>(result);
            return GenerateSuccessResponse<IEnumerable<CartResponse>>(resultMapping);
        }

        public async Task<Response<CartResponse>> GetById(int id)
        {
            var result = await _unitOfWork.CartRepository.GetTableNoTracking().Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (result == null)
                return GenerateNotFoundResponse<CartResponse>();
            var resultMapping = _mapper.Map<CartResponse>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<Response<string>> Update(CartRequest cartRequest, int id)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdIncludeCartItemAsync(id);

            if (cart == null)
                return GenerateNotFoundResponse<string>();

            var productIds = cartRequest.CartItems.Select(p => p.ProductId).ToList();
            var products = await _unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                return GenerateNotFoundResponse<string>("One or more products not found.");

            cart.AccountId = cartRequest.AccountId;

            var existingCartItems = cart.CartItems.ToList();

            var newCartItems = cartRequest.CartItems.Select(p => _mapper.Map<CartItem>(p)).ToList();


            var itemsToRemove = existingCartItems
                .Where(existing => !newCartItems.Any(newItem => newItem.ProductId == existing.ProductId))
                .ToList();
            foreach (var item in itemsToRemove)
            {
                cart.CartItems.Remove(item);
                await unitOfWork.CartItemRepository.DeleteByIdAsync(item);
                await unitOfWork.CompleteAsync();
            }

            foreach (var newItem in newCartItems)
            {
                var existingItem = existingCartItems.FirstOrDefault(e => e.ProductId == newItem.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += newItem.Quantity;
                }
                else
                {
                    cart.CartItems.Add(newItem);
                }
            }

            unitOfWork.CartRepository.Update(cart);
            await unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("");
        }
    }
}
