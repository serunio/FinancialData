using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using System.Net.Http.Json;

namespace FinancialData.WebClient.Services
{
    public class RecordService
    {
        private readonly HttpClient _httpClient;

        public RecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Record>> GetRecordsAsync(SelectionResult selectionResult)
        {
            var response = await _httpClient.PostAsJsonAsync("api/GetRecords", selectionResult);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Record>>() ?? [];
            }
            else
            {
                throw new Exception($"Błąd przy dodawaniu danych:{response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }
}
