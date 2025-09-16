using FinancialData.API.Services;
using FinancialData.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinancialData.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RecordController(RecordService service) : ControllerBase
    {
        [HttpPost("GetRecords")]
        public async Task<IActionResult> GetDataFromJson([FromBody]SelectionResult selectionResult)
        {
            var records = await service.GetRecords(selectionResult);
            return Ok(records);
        }

        [HttpPost("AddRecords")]
        public async Task<IActionResult> AddRecords(IFormFile file)
        {
            try
            {
                await service.AddRecords(file); 
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok();
        }

        [HttpPost("RemoveRecords")]
        public async Task<IActionResult> RemoveRecords([FromBody] SelectionResult selectionResult)
        {
            await service.RemoveRecords(selectionResult);
            return Ok();
        }
    }
}
