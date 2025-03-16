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
        public async Task<IActionResult> Frequencies(int DataTypeId = 0, int PresentationTypeId = 0)
        {
            var data = await _service.GetFrequenciesAsync(DataTypeId, PresentationTypeId);
            return Ok(data);
        }
        [HttpGet("PresentationTypes")]
        public async Task<IActionResult> PresentationTypes(int DataTypeId = 0, int FrequencyId = 0)
        {
            var data = await _service.GetPresentationTypesAsync(DataTypeId, FrequencyId);
            return Ok(data);
        }
    }
}
