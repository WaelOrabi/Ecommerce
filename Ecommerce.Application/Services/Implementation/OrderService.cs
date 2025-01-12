using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Order;
using Ecommerce.Application.Resources;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.Services.Implementation
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer) : ResponseHandler(localizer), IOrderService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<SharedResources> _localizer = localizer;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Add(OrderRequest orderRequest)
        {
            //var account = await _unitOfWork.AccountRepository.GetByIdAsync(orderRequest.AccountId);
            //if (account == null)
            //    return GenerateNotFoundResponse<string>();
            var productIds = orderRequest.OrderProducts.Select(p => p.ProductId).ToList();
            var products = await unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                return GenerateNotFoundResponse<string>("One or more products not found.");
            decimal totalPrice = 0;
            foreach (var product in products)
            {
                var orderedProduct = orderRequest.OrderProducts.First(p => p.ProductId == product.Id);
                totalPrice += product.Price * orderedProduct.Quantity;
            }
            int numberProducts = orderRequest.OrderProducts.Count;

            var order = _mapper.Map<Order>(orderRequest);
            order.TotalPrice = totalPrice;
            order.NumberProducts = numberProducts;
            order.CreateAt = DateTime.Now;
            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.CompleteAsync();
            return GenerateCreatedResponse("");
        }

        public async Task<Response<int>> Delete(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                return GenerateNotFoundResponse<int>();
            await unitOfWork.OrderRepository.DeleteByIdAsync(order);
            await unitOfWork.CompleteAsync();
            return GenerateDeleteResponse<int>();
        }

        public async Task<Response<IEnumerable<OrderResponse>>> GetAll()
        {
            var result = await unitOfWork.OrderRepository.GetTableNoTracking().Include(x => x.OrderProducts).ToListAsync();
            var resultMapping = _mapper.Map<IEnumerable<OrderResponse>>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<Response<OrderResponse>> GetById(int id)
        {
            var result = await unitOfWork.OrderRepository.GetTableNoTracking().Include(x => x.OrderProducts).FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (result == null)
                return GenerateNotFoundResponse<OrderResponse>();
            var resultMapping = _mapper.Map<OrderResponse>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<Response<string>> Update(OrderRequest orderRequest, int id)
        {


            var order = await unitOfWork.OrderRepository.GetTableNoTracking()
                .Include(x => x.OrderProducts)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return GenerateNotFoundResponse<string>();


            var productIds = orderRequest.OrderProducts.Select(p => p.ProductId).ToList();
            var products = await unitOfWork.ProductRepository.GetAllAsync(p => productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                return GenerateNotFoundResponse<string>("One or more products not found.");



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

            var existingOrderProducts = order.OrderProducts.ToList();



            foreach (var requestProduct in orderRequest.OrderProducts)
            {
                var existingProduct = existingOrderProducts.FirstOrDefault(p => p.ProductId == requestProduct.ProductId);
                if (existingProduct != null)
                {

                    existingProduct.Quantity = requestProduct.Quantity;
                }
                else
                {
                    var newOrderProduct = _mapper.Map<OrderProduct>(requestProduct);
                    existingOrderProducts.Add(newOrderProduct);
                }
            }

            order.OrderProducts = existingOrderProducts;
            unitOfWork.OrderRepository.Update(order);
            await unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("");
        }

    }
}
