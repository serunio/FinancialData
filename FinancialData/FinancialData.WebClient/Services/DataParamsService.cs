using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using FinancialData.Shared.Models;

namespace FinancialData.WebClient.Services
{
    public class DataParamsService(HttpClient httpClient)
    {
        public async Task<Dictionary<int, string>> GetDictAsync<T>(int DataTypeId, int FrequencyId, int PresentationTypeId) where T : class, IParam
        {
            return (await GetParams<T>(DataTypeId, FrequencyId, PresentationTypeId)).ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<List<T>> GetParams<T>(int DataTypeId = 0, int FrequencyId = 0, int PresentationTypeId = 0) where T : class, IParam
        {
            var response = await httpClient.GetAsync("api/" + T.Link + "?DataTypeId=" + DataTypeId + "&FrequencyId=" + FrequencyId + "&PresentationTypeId=" + PresentationTypeId);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<T>>() ?? [];
            }
            throw new Exception($"Błąd przy pobieraniu danych:{response.StatusCode} {response.ReasonPhrase}");

        }
    }
}
