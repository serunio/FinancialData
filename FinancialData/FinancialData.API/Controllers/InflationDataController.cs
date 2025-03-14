using FinancialData.API.Services;
using FinancialData.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FinancialData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InflationDataController : ControllerBase
    {
        private InflationDataService _dataService;

        public InflationDataController(InflationDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetInflationData()
        {
            var data = await _dataService.GetAllInflationDataAsync();
            return Ok(data);
        }

        [HttpGet("byData")]
        public async Task<IActionResult> GetInflationDataByDate(DateTime startDate, DateTime endDate)
        {
            var data = await _dataService.GetInflationDataByDateAsync(startDate, endDate);
            return Ok(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddInflationData([FromBody] InflationDataDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _dataService.AddInflationDataAsync(dto);
            return CreatedAtAction(nameof(AddInflationData), null);
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Plik jest pusty");
            }

            using var stream = file.OpenReadStream();
            await _dataService.ImportFromExcelAsync(stream);
            return Ok("Import zakończony sukcesem");
        }

        [HttpDelete]
        public async Task<IActionResult> ClearInflationData()
        {
            await _dataService.ClearInflationDataAsync();
            return Ok();
        }

    }
}
