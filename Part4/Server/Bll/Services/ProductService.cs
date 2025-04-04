using Bll.Interfaces;
using Dal.Dto;
using Dal.Entity;
using Dal.Interfaces;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bll.Services
{
    public class ProductService :IProductService
    {
        private readonly IProductRepository _ProductRepository;
       
        public ProductService(IProductRepository productRepository) {
            _ProductRepository = productRepository;
           
        }

        public async Task AddProductAsync(ProductDto product,int supplierId)
        {
            var productToAdd = new ProductEntity 
            {
                Name = product.Name,
                Price = product.Price,
                MinimumQuantity = product.MinimumQuantity,
                SupplierId = supplierId
            };
            await _ProductRepository.AddProductAsync(productToAdd);
        }

        
        public async Task<List<ProductEntity>> GetAllProductAsync()
        {
            return await _ProductRepository.GetAllProductAsync();
            //List<ItemDto> result = new List<ItemDto>();
            //var itemTemp = await _itemRepository.GetAllItemAsync();
            //foreach ( var item in itemTemp ) 
            //{
            //    //List<byte[]> images = new List<byte[]>();
            //    byte[] images = await _imageService.GetFirstImageByItemIdAsync(item.ItemId);
            //    var categoryName = await _categoryService.GetCategoryNameById(item.CategoryId);
            //    result.Add(new ItemDto()
            //    {
            //        ItemId = item.ItemId,
            //        ItemEnName =item.ItemEnName,
            //        ItemHeName =item.ItemHeName,
            //        Price = item.Price,
            //        CategoryId = item.CategoryId,
            //        CategoryName = categoryName,
            //        Details = item.Details,
            //        AverageSize = item.AverageSize,
            //        Images = images 

            //    }); ;

            //}

        }

        public async Task<List<ProductEntity>> GetProductBySupplierIdAsync(int supplierId)
        {
            return await _ProductRepository.GetProductBySupplierAsync(supplierId);
            //List<ItemDto> result = new List<ItemDto>();
            //var itemTemp = await _itemRepository.GetItemByCategoryIdAsync(categoryId);

            //var categoryName = await _categoryService.GetCategoryNameById(categoryId);
            //foreach ( var item in itemTemp )
            //{
                //List<byte[]> images = new List<byte[]>();
                //images = await _imageService.GetImageByItemIdAsync(item.ItemId);
            //    byte[] image = await _imageService.GetFirstImageByItemIdAsync(item.ItemId);
            //    List<CuttingShapeEntity> cutting = await _cuttingShapePerItemService.GetAllCuttingShapePerItemByItemIdAsync(item.ItemId);
            //    List<CuttingShapeDto> cuttingShapeList = new List<CuttingShapeDto>();

            //    if (cutting != null) { 

            //        foreach (var cuttingShape in cutting) 
            //        {
            //            cuttingShapeList.Add(new CuttingShapeDto()
            //            {
            //                Id = cuttingShape.CuttingShapeId,
            //                EnName = cuttingShape.ShapeEnName,
            //                HeName = cuttingShape.ShapeHeName,
            //            }); ;
                    
            //         }
            //    }
                
            //    result.Add(new ItemDto()
            //    {
            //        ItemId = item.ItemId,
            //        ItemEnName = item.ItemEnName,
            //        ItemHeName = item.ItemHeName,
            //        Price = item.Price,
            //        CategoryId = item.CategoryId,
            //        CategoryName = categoryName,
            //        Details = item.Details,
            //        AverageSize = item.AverageSize,
            //        Images = image,
            //        cuttingShapes = cuttingShapeList,

            //    }); ;
            //}
           

        }

        //public async Task<ItemDto> GetItemByItemIdAsync(int itemId)
        //{
        //    ItemDto result = new ItemDto();
        //    //List<byte[]> images = new List<byte[]>();
        //    byte[] images = await _imageService.GetFirstImageByItemIdAsync(itemId);
        //    var itemTemp= await _itemRepository.GetItemByItemIdAsync(itemId);
        //    result.ItemId = itemId;
        //    result.ItemEnName = itemTemp.ItemEnName;
        //    result.ItemHeName = itemTemp.ItemHeName;
        //    result.Price = itemTemp.Price;
        //    result.CategoryId = itemTemp.CategoryId;
        //    result.CategoryName = await _categoryService.GetCategoryNameById(itemTemp.CategoryId);
        //    result.Details = itemTemp.Details;  
        //    result.AverageSize = itemTemp.AverageSize;
        //    result.Images = images;
        //    return result;
        //}

    }
}
