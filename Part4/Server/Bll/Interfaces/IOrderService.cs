using Dal.Entity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderToAddDto order);
        Task UpdateOrderAsync(int id,int statusIs);

        Task<List<OrderEntity>> GetAllOrderAsync();
        Task<List<OrderEntity>> GetOrderByStatusAsync(int statusId);

        Task<List<OrderEntity>> GetOrderBySupplierAsync(int supplierId);
    }
}
