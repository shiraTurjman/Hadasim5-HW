using Bll.Interfaces;
using Dal.Entities;
using Dal.Entity;
using Dal.Interfaces;
using Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        public OrderService(IOrderRepository orderRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
        }

        public async Task AddOrderAsync(OrderToAddDto order)
        {
            OrderEntity orderEntity = new OrderEntity
            { 
                OrderDate = DateTime.Now,
                Quantity = order.Quantity,
                ProductId = order.ProductId,
                StatusId = 1
            };
            await _orderRepository.AddOrderAsync(orderEntity);
        }

        public async Task<List<OrderEntity>> GetAllOrderAsync()
        {
           return await _orderRepository.GetAllOrderAsync();
            
        }

        public async Task<List<OrderEntity>> GetOrderByStatusAsync(int statusId)
        {
            return await _orderRepository.GetOrderByStatusAsync(statusId);
        }

        public async Task<List<OrderEntity>> GetOrderBySupplierAsync(int supplierId)
        {
            return await _orderRepository.GetOrderBySupplierAsync(supplierId);
        }

        public async Task UpdateOrderAsync(int id,int statusId)
        {
            await _orderRepository.UpdateOrderAsync(id,statusId);
            if (statusId == 3) 
            {

                OrderEntity order = await _orderRepository.GetOrderByIdAsync(id);
                ItemEntity item = await _itemRepository.GetItemByNameAsync(order.Product.Name);
                item.Amount += order.Quantity;
                await _itemRepository.UpdateItemAsync(item);
            }
        }
    }
}
