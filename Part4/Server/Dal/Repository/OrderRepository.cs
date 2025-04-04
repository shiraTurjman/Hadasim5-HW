using Dal.Entity;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly IDbContextFactory<ServerDbContext> _factory;
        public OrderRepository(IDbContextFactory<ServerDbContext> factory)
        {
            _factory = factory;
        }

       public async Task AddOrderAsync(OrderEntity order)
        {
            using var context=_factory.CreateDbContext();
            await context.Orders.AddAsync(order);
            context.SaveChanges();
        }

         public async Task<List<OrderEntity>> GetAllOrderAsync()
         {
            using var context=_factory.CreateDbContext();
            return await context.Orders.ToListAsync();
         }

         public async Task<List<OrderEntity>> GetOrderBySupplierAsync(int supplierId)
        {
           using var context= _factory.CreateDbContext();
            List<OrderEntity> orders = await context.Orders.Where(o => o.Product.SupplierId == supplierId).ToListAsync();
            if (orders != null)
            {
                return orders;
            }
            else
            {
                throw new Exception("orders nor found");
            }
        }


        public async Task<List<OrderEntity>> GetOrderByStatusAsync(int statusId)
        {
            using var dbContext = _factory.CreateDbContext();
            List<OrderEntity> order = await dbContext.Orders.Where(o => o.StatusId == statusId).ToListAsync();
            if (order != null)
            {
                return order;
            }
            else
            {
                throw new Exception("order not found");
            }

        }

       public async Task UpdateOrderAsync(OrderEntity order)
        {
            using var dbContext = _factory.CreateDbContext();
            var orderToUpdate = dbContext.Orders.FirstOrDefault(i => i.Id == order.Id);
            if (orderToUpdate != null)
            {
                orderToUpdate.Status = order.Status;
                await dbContext.SaveChangesAsync();

            }
            else { throw new Exception("could not update order , order not found"); }
        }

        
    }

}
