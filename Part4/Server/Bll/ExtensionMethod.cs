using Bll.Interfaces;
using Bll.Services;
using Dal.Entity;
using Dal.Interfaces;
using Dal.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public static class ExtensionMethod
    {
        public static void InitDI(IServiceCollection service, string connectionString)
        {
            service.AddPooledDbContextFactory<ServerDbContext>(item => item.UseSqlServer(connectionString));

            //dependency injection 
            //repository-dal
            
            service.AddScoped<IStatusRepository, StatusRepository>();
            
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            
            service.AddScoped<ISupplierRepository, SupplierRepository>();

            //service-bll
            
            
            service.AddScoped<IStatusService, StatusService>();

            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IOrderService, OrderService>();
            
            service.AddScoped<ISupplierService, SupplierService>();


        }
    }
}
