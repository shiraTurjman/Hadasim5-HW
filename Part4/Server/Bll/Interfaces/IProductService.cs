using Dal.Dto;
using Dal.Entity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IProductService
    {
        Task AddProductAsync(ProductDto product,int supplierId);
        
        Task<List<ProductEntity>> GetAllProductAsync();
        Task<List<ProductEntity>> GetProductBySupplierIdAsync(int supplierId);
        //Task<ItemDto> GetItemByItemIdAsync(int itemId);
    }
}
