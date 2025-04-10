﻿using Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(OrderEntity order);
        Task UpdateOrderAsync(int id,int statusId);
        
        Task<List<OrderEntity>> GetAllOrderAsync();
        Task<List<OrderEntity>> GetOrderByStatusAsync(int statusId);

        Task<List<OrderEntity>> GetOrderBySupplierAsync(int supplierId);

        Task<OrderEntity> GetOrderByIdAsync(int id);
    }
}
