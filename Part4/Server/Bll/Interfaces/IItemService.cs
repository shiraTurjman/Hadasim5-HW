using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IItemService
    {
        Task AddItemAsync(ItemEntity item);

        Task<string> OnReceiptReceivedAsync(JsonElement data);
    }
}
