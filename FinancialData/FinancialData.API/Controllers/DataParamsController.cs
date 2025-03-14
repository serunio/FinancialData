using FinancialData.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialData.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class DataParamsController(DataParamsService service) : ControllerBase
    {
        private DataParamsService _service = service;

        [HttpGet("DataTypes")]
        public async Task<IActionResult> DataTypes()
        {
            var data = await _service.GetAllDataTypesAsync();
            return Ok(data);
        }
        [HttpGet("Frequencies")]
        public async Task<IActionResult> Frequencies()
        {
            var data = await _service.GetAllFrequenciesAsync();
            return Ok(data);
        }
        [HttpGet("PresentationTypes")]
        public async Task<IActionResult> PresentationTypes()
        {
            var data = await _service.GetAllPresentationTypesAsync();
            return Ok(data);
        }
    }
}
