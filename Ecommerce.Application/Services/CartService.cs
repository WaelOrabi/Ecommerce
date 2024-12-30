using Application.Interfaces;
using AutoMapper;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Application.Services
{
    public class CartService(IUnitOfWork unitOfWork, IMapper mapper) : ICartService
    {
        private readonly IMapper _mapper = mapper;

        public async Task<CartResponse> Add(CartRequestDTO cartRequest)
        {
            var account = await unitOfWork.AccountRepository.GetByIdAsync(cartRequest.AccountId);
            if (account == null)
            {

                throw new Exception("Account not found.");
            }
            var productIds = cartRequest.CartItems.Select(p => p.ProductId).ToList();
            var products = await unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                throw new Exception("One or more products not found.");
            //var cart = new Cart
            //{
            //    AccountId = cartRequest.AccountId,
            //    CartItems = cartRequest.CartItems.Select(p => _mapper.Map<CartItem>(p)
            //    //new CartItem
            //    //{
            //    //    ProductId = p.ProductId,
            //    //    Quantity = p.Quantity,
            //    //}
            //    ).ToList(),
            //};
            var cart = _mapper.Map<Cart>(cartRequest);
            var result = await unitOfWork.CartRepository.AddAsync(cart);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<CartResponse>(result);
        }

        public async Task<int> Delete(int id)
        {
            await unitOfWork.CartRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<CartResponse>> GetAll()
        {
            var result = await unitOfWork.CartRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CartResponse>>(result);
        }

        public async Task<CartResponse> GetById(int id)
        {
            var result = await unitOfWork.CartRepository.GetByIdAsync(id);
            return _mapper.Map<CartResponse>(result);
        }

        public async Task<CartResponse?> Update(CartRequestDTO cartRequest, int id)
        {
            var cart = await unitOfWork.CartRepository.GetByIdIncludeCartItemAsync(id);

            if (cart == null)
                return null;

            var productIds = cartRequest.CartItems.Select(p => p.ProductId).ToList();
            var products = await unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                throw new Exception("One or more products not found.");
            cart.AccountId = cartRequest.AccountId;

            var existingCartItems = cart.CartItems.ToList();

            var newCartItems = cartRequest.CartItems.Select(p => _mapper.Map<CartItem>(p)
            //new CartItem
            //{
            //    ProductId = p.ProductId,
            //    Quantity = p.Quantity,
            //    CartId = id
            //}
            ).ToList();


            var itemsToRemove = existingCartItems
                .Where(existing => !newCartItems.Any(newItem => newItem.ProductId == existing.ProductId))
                .ToList();
            foreach (var item in itemsToRemove)
            {
                cart.CartItems.Remove(item);
                await unitOfWork.CartItemRepository.DeleteByIdAsync(item.Id);
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

            var result = unitOfWork.CartRepository.Update(cart);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<CartResponse>(result);
        }
    }
}
