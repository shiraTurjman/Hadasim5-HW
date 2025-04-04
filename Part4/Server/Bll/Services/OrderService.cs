using Bll.Interfaces;
using Dal.Entity;
using Dal.Interfaces;
using Dto;
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
        public OrderService(IOrderRepository orderRepository) {
        _orderRepository = orderRepository;

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

        public async Task UpdateOrderAsync(OrderEntity order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
