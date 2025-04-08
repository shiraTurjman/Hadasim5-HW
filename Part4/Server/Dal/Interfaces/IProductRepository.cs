using Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(ProductEntity product);
       
    
        Task<List<ProductEntity>> GetAllProductAsync();
        Task<List<ProductEntity>> GetProductBySupplierAsync(int supplierId);
        Task<ProductEntity> GetCheapestProduct(string name);
        //Task<ProductEntity> GetProductByProductIdAsync(int productId);

    }
}
