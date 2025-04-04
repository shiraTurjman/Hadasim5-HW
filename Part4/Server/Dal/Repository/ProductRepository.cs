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
    public class ProductRepository : IProductRepository

    {
        private readonly IDbContextFactory<ServerDbContext> _factory;

        public ProductRepository (IDbContextFactory<ServerDbContext> factory)
        {
            _factory = factory;
        }

        public async Task AddProductAsync(ProductEntity product)
        {
            using var context = _factory.CreateDbContext();
            context.Products.Add(product);
            await context.SaveChangesAsync();
            
        }

       

       public async Task<List<ProductEntity>> GetAllProductAsync()
        {
            using var context = _factory.CreateDbContext();
            var list =await context.Products.ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                throw new Exception("could not get items ");
            }
        }

        public async Task<List<ProductEntity>> GetProductBySupplierAsync(int supplierId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.Products.Where(p => p.SupplierId == supplierId).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                throw new Exception("could not get Product by supplier");
            }
        }

        // public async Task<ProductEntity> GetItemByItemIdAsync(int itemId)
        //{
        //    using var dbContext = _factory.CreateDbContext();
        //    var item = dbContext.Items.FirstOrDefault(i=>i.ItemId==itemId);
        //    if (item == null) 
        //    {
        //        throw new Exception("could not find item");
        //    }
        //    return item;
        //}

       
    }
}
