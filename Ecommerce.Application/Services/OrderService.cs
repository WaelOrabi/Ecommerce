using Application.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Application.Services
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
    {
        private readonly IMapper _mapper = mapper;

        public async Task<OrderResponse> Add(OrderRequestDTO orderRequest)
        {
            var account = await unitOfWork.AccountRepository.GetByIdAsync(orderRequest.AccountId);
            if (account == null)
            {

                throw new Exception("Account not found.");
            }
            var productIds = orderRequest.OrderProducts.Select(p => p.ProductId).ToList();
            var products = await unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                throw new Exception("One or more products not found.");
            decimal totalPrice = 0;
            foreach (var product in products)
            {
                var orderedProduct = orderRequest.OrderProducts.First(p => p.ProductId == product.Id);
                totalPrice += product.Price * orderedProduct.Quantity;
            }
            int numberProducts = orderRequest.OrderProducts.Count;
            //var order = new Order
            //{
            //    AccountId = orderRequest.AccountId,
            //    TotalPrice = totalPrice,
            //    NumberProducts = numberProducts,
            //    CreateAt = DateTime.Now,
            //    OrderProducts = orderRequest.OrderProducts.Select(p => new OrderProduct
            //    {
            //        ProductId = p.ProductId,
            //        Quantity = p.Quantity
            //    }).ToList()
            //};
            var order = _mapper.Map<Order>(orderRequest);
            order.TotalPrice = totalPrice;
            order.NumberProducts = numberProducts;
            order.CreateAt = DateTime.Now;
            var result = await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResponse>(result);
        }

        public async Task<int> Delete(int id)
        {
            await unitOfWork.OrderRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<OrderResponse>> GetAll()
        {
            var result = await unitOfWork.OrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponse>>(result);
        }

        public async Task<OrderResponse> GetById(int id)
        {
            var result = await unitOfWork.OrderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderResponse>(result);
        }

        public async Task<OrderResponse?> Update(OrderRequestDTO orderRequest, int id)
        {
            var order = await unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                return null;

            var productIds = orderRequest.OrderProducts.Select(p => p.ProductId).ToList();
            var products = await unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                throw new Exception("One or more products not found.");
            decimal totalPrice = 0;
            foreach (var product in products)
            {
                var orderedProduct = orderRequest.OrderProducts.First(p => p.ProductId == product.Id);
                totalPrice += product.Price * orderedProduct.Quantity;
            }
            int numberProducts = orderRequest.OrderProducts.Count;

            order.TotalPrice = totalPrice;
            order.NumberProducts = numberProducts;
            order.CreateAt = DateTime.Now;
            order.OrderProducts = orderRequest.OrderProducts.Select(p => new OrderProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList();
            var result = unitOfWork.OrderRepository.Update(order);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResponse>(result);
        }
    }
}
