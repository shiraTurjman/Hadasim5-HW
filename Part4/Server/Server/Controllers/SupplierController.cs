using Bll.Interfaces;
using Bll.Services;
using Dal.Dto;
using Dal.Entity;
using Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpPost("AddSupplier")]

        public async Task<ActionResult<int>> AddSupplier([FromBody] SupplierToAddDto supplier)
        {
            try
            {
                if (supplier == null || string.IsNullOrEmpty(supplier.Phone))
                {
                    return BadRequest("Invalid supplier data. Make sure all fields are provided.");
                }

                Console.WriteLine($"Adding supplier: {supplier.Phone}");

                int id = await _supplierService.AddSupplierAsync(supplier);
                return Ok(id);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error in AddSupplier: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("GetSupplierById/{id}")]

        public async Task<ActionResult<SupplierEntity>> GetSupplierById(int id)
        {
            try
            {
                return await _supplierService.GetSupplierByIdAsync(id);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<SupplierDto>> LoginSupplierAsync([FromBody] string phone)
        {
            try
            {
                return await _supplierService.LoginAsync(phone);
            }
            catch (Exception ex)
            {
                //return Unauthorized(ex);
                return BadRequest("phone not valid. ");
            }
        }

    }
}
