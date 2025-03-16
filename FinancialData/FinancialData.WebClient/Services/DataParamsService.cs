using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using FinancialData.Shared.Models;

namespace FinancialData.WebClient.Services
{
    public class DataParamsService
    {
        private readonly HttpClient _httpClient;

        public DataParamsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Dictionary<int, string>> GetDictAsync<T>(int DataTypeId = 0, int FrequencyId = 0, int PresentationTypeId = 0) where T : class, IParam
        {
            var response = await _httpClient.GetAsync("api/" + T.Link + "?DataTypeId=" + DataTypeId + "&FrequencyId=" + FrequencyId + "&PresentationTypeId=" + PresentationTypeId);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<T>>() ?? [];
                return data.ToDictionary(x => x.Id, x => x.Name);
            }
            else
            {
                throw new Exception($"Błąd przy pobieraniu danych:{response.StatusCode} {response.ReasonPhrase}");
            }
        }

    }
}
