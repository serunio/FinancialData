using System.Net;
using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

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

        public async Task AddRecordsAsync(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.OpenReadStream()), "file", file.Name);
            var response = await _httpClient.PostAsync("api/AddRecords", content);
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new Exception(await response.Content.ReadAsStringAsync());
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Błąd przy dodawaniu danych:{response.StatusCode} {response.ReasonPhrase}");
        }

        public async Task RemoveRecordsAsync(SelectionResult? selectionResult)
        {
            if (selectionResult == null)
                return;
            var response = await _httpClient.PostAsJsonAsync("api/RemoveRecords", selectionResult);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Błąd przy usuwaniu danych:{response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }
}
