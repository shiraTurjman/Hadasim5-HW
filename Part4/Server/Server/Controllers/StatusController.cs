using Bll.Interfaces;
using Bll.Services;
using Dal.Entities;
using Dal.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        { _statusService = statusService; }

        [HttpGet("GetAllStatus")]
        public async Task<ActionResult<List<StatusEntity>>> getAllStatus()
        {
            try
            {
                return Ok(await _statusService.GetAllStatusAsync());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost("AddStatus")]
        public async Task<ActionResult<int>> AddStatus([FromBody] StatusEntity status)
        {
            try
            {
                await _statusService.AddStatusAsync(status);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
