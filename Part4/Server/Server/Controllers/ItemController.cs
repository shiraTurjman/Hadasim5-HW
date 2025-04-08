using Bll.Interfaces;
using Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class ItemController : ControllerBase
        {
            private readonly IItemService _itemService;

            public ItemController(IItemService itemService)
            { _itemService = itemService; }



        [HttpPost("AddItem")]
        public async Task<ActionResult> AddItem([FromBody] ItemEntity item)
        {
            try
            {
                await _itemService.AddItemAsync(item);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("OnReceiptReceived")]
        public async Task<ActionResult> OnReceiptReceived([FromBody] JsonElement data)
        {
            var result = await _itemService.OnReceiptReceivedAsync(data);
            return Ok(result);
        }
    }
    }

