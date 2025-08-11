using FinancialData.API.Data;
using FinancialData.API.Services;
using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialData.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RecordController(RecordService service) : ControllerBase
    {
        private readonly RecordService _service = service;

        [HttpPost("GetRecords")]
        public async Task<IActionResult> GetDataFromJson([FromBody]SelectionResult selectionResult)
        {
            var records = await _service.GetRecords(selectionResult);
            return Ok(records);
        }

        [HttpPost("AddRecords")]
        public async Task<IActionResult> AddRecords(IFormFile file)
        {
            await _service.AddRecords(file);
            return Ok();
        }

        [HttpPost("RemoveRecords")]
        public async Task<IActionResult> RemoveRecords([FromBody] SelectionResult selectionResult)
        {
            await _service.RemoveRecords(selectionResult);
            return Ok();
        }
    }
}
