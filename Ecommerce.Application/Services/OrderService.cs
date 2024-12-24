using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class OrderService (IUnitOfWork unitOfWork): IOrderService
    {
        public async Task<Order> Add(OrderRequest orderRequest)
        {
            var account = await unitOfWork.AccountRepository.GetByIdAsync(orderRequest.AccountId);
            if (account == null)
            {

                throw new Exception("Account not found.");
            }
            var productIds=orderRequest.OrderProducts.Select(p=>p.ProductId).ToList();
            var products=await unitOfWork.ProductRepository.GetAllAsync(p=>productIds.Contains(p.Id));
            if (products.Count != productIds.Count)
                throw new Exception("One or more products not found.");
            decimal totalPrice = 0;
            foreach (var product in products) { 
            var orderedProduct=orderRequest.OrderProducts.First(p=>p.ProductId==product.Id);
                totalPrice += product.Price * orderedProduct.Quantity;
            }
            int numberProducts=orderRequest.OrderProducts.Count;
            var order = new Order {
            AccountId=orderRequest.AccountId,
            TotalPrice=totalPrice,
            NumberProducts=numberProducts,
            CreateAt=DateTime.Now,
                OrderProducts = orderRequest.OrderProducts.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };
            var result=await unitOfWork.OrderRepository.AddAsync(order);
           await unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
             await unitOfWork.OrderRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await unitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<Order> GetById(int id)
        {
          return await unitOfWork.OrderRepository.GetByIdAsync(id);
        }

        public async Task<Order> Update(OrderRequest orderRequest)
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
            var order = new Order
            {
                AccountId = orderRequest.AccountId,
                TotalPrice = totalPrice,
                NumberProducts = numberProducts,
                CreateAt = DateTime.Now,
                OrderProducts = orderRequest.OrderProducts.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };
            var result =  unitOfWork.OrderRepository.Update(order);
            await unitOfWork.CompleteAsync();
            return result;
        }
    }
}
