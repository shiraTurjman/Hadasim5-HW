using Bll.Interfaces;
using Dal.Dto;
using Dal.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        { _productService = productService; }

        //[HttpPost("AddProduct")]

        //public async Task<ActionResult<int>> AddItem([FromBody] AddItemDto item)
        //{
        //    try
        //    {
        //        int id = await _itemService.AddItemAsync(item);
        //        return Ok(id);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        

        [HttpGet("GetAllProduct")]
        public async Task<ActionResult<List<ProductEntity>>> GetAllProduct()
        {
            try
            {
                return await _productService.GetAllProductAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [HttpGet("GetProductBySupplier/{id}")]
        public async Task<ActionResult<List<ProductEntity>>> GetProductBySupplier(int id)
        {
            try
            {
                return await _productService.GetProductBySupplierIdAsync(id);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        //[HttpGet("GetItemByItem/{id}")]
        //public async Task<ActionResult<ItemDto>> GetItemByItem(int id)
        //{
        //    try 
        //    {
        //        return await _itemService.GetItemByItemIdAsync(id);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

    }
}
