using Dal.Entities;
using Dal.Entity;
using Dal.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository
{
    
    public class ItemRepository : IItemRepository
    {
        private readonly IDbContextFactory<ServerDbContext> _factory;
        public ItemRepository(IDbContextFactory<ServerDbContext> factory)
        {
            _factory = factory;
        }
        public async Task AddItemAsync(ItemEntity item)
        {
            using var context = _factory.CreateDbContext();
            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task<ItemEntity> GetItemByNameAsync(string name)
        {
            using var context = _factory.CreateDbContext();
            var item =await context.Items.FirstAsync(i => i.Name.Equals(name));
            if (item != null) {
                return item;
            }
            throw new Exception("Could not found item");

        }

        public async Task UpdateItemAsync(ItemEntity item)
        {

            using var context = _factory.CreateDbContext(); 
            var itemToUpdate = context.Items.FirstOrDefault(i => i.Id  == item.Id);
            if (itemToUpdate != null) { 
                itemToUpdate.Amount = item.Amount;
                await context.SaveChangesAsync();
            }

            throw new NotImplementedException();
        }
    }
}
