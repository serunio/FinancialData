using FinancialData.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinancialData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbwController : ControllerBase
    {
        private static readonly HttpClient Client = new HttpClient()
        {
            BaseAddress = new Uri("https://api-dbw.stat.gov.pl/api/variable/")
        };
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = new JsonKebabCasePolicy()
        };
        // GET: api/<DbwController>
        [HttpGet("SingleRecord")]
        public async Task<IActionResult> GetSingleRecord(int zmienna, int przekroj, int okres, int prezentacja, int rok, int? wymiar1, int? pozycja1, int? wymiar2, int? pozycja2, int? wymiar3, int? pozycja3, int? wymiar4, int? pozycja4)
        {
            var DbwObject = new DbwData()
            {
                IdZmienna = zmienna,
                IdPrzekroj = przekroj,
                IdOkres = okres,
                IdSposobPrezentacjiMiara = prezentacja,
                IdDaty = rok,
                IdWymiar1 = wymiar1,
                IdPozycja1 = pozycja1,
                IdWymiar2 = wymiar2,
                IdPozycja2 = pozycja2,
                IdWymiar3 = wymiar3,
                IdPozycja3 = pozycja3,
                IdWymiar4 = wymiar4,
                IdPozycja4 = pozycja4
            };
            using HttpResponseMessage response = await Client.GetAsync(
                $"variable-data-section" +
                          $"?id-zmienna={DbwObject.IdZmienna}" +
                          $"&id-przekroj={DbwObject.IdPrzekroj}" +
                          $"&id-rok={DbwObject.IdDaty}" +
                          $"&id-okres={DbwObject.IdOkres}");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            var obj =  JsonSerializer.Deserialize<Root>(responseData, Options);
            var value = obj?.Data.Find(x => x.Matches(DbwObject))?.Wartosc;
            if (value is null)
                return NotFound();
            return Ok(value);
        }

        [HttpGet("EveryPrzekroj")]
        public async Task<IActionResult> GetEveryPrzekroj()
        {
            Dictionary<int, string?>? dict = [];
            for (var i = 0; i <= 1; i++)
            {
                using HttpResponseMessage response =
                    await Client.GetAsync($"variable-section-periods?ile-na-stronie=5000&numer-strony={i}&lang=pl");
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();
                var obj = JsonSerializer.Deserialize<Root>(responseData, Options);
                var d = obj?.Data
                    .GroupBy(x => x.IdPrzekroj)
                    .ToDictionary(x => x.Key, x => x.First().NazwaPrzekroj);
                dict = dict.Concat(d ?? new Dictionary<int, string?>()).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Last().Value);
            }

            return Ok(dict);
        }

        [HttpGet("ZmiennePrzekroju")]
        public async Task<IActionResult> GetZmiennePrzekroju(int idPrzekroj)
        {
            
            using HttpResponseMessage response = 
                await Client.GetAsync($"variable-section-periods?ile-na-stronie=5000&numer-strony=0&lang=pl");
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<Root>(responseData, Options);
            var d = obj?.Data
                .Where(x => x.IdPrzekroj == idPrzekroj).Select(x => new { x.IdZmienna, x.NazwaZmienna }).Distinct();
             
            return Ok(d);
        }
    }
}
