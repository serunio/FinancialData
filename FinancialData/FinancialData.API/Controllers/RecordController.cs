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
        public async Task<IActionResult> GetDataFromJson([FromBody]GetRecordsDto getRecordsDto)
        {
            var records = await _service.GetRecords(getRecordsDto);
            return Ok(records);
        }
    }
}
