using Bll.Interfaces;
using Bll.Services;
using Dal.Entity;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<int>> AddOrder([FromBody] OrderToAddDto order)
        {
            try
            {
                await _orderService.AddOrderAsync(order);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllOrder")]
        public async Task <ActionResult<List<OrderEntity>>> getAllCategories()
        {
            try 
            {
                return Ok(await _orderService.GetAllOrderAsync());
            
             } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpGet("GetOrderByStatus/{id}")]
        public async Task<ActionResult<List<OrderEntity>>> GetOrderByStatus(int id)
        {
            try
            {
                return Ok(await _orderService.GetOrderByStatusAsync(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetOrderBySupplier/{id}")]
        public async Task<ActionResult<List<OrderEntity>>> GetOrderBySupplier(int id)
        {
            try
            {
                return Ok(await _orderService.GetOrderBySupplierAsync(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("UpdateOrder/{id}/{statusId}")]
        public async Task<ActionResult> UpdateOrder(int id,int statusId)
        {
            try
            {
                await _orderService.UpdateOrderAsync(id,statusId);
                return Ok(true);
            }
            catch (Exception ex)
            { 
            throw new Exception(ex.Message);
            }
        }
        
    }
}
