using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IItemRepository
    {
        //Task<List<StatusEntity>> GetAllStatusAsync();

        Task AddItemAsync(ItemEntity item);

        Task UpdateItemAsync(ItemEntity item);

        Task<ItemEntity> GetItemByNameAsync(string name);
    }
}
