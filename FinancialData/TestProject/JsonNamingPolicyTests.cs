using FinancialData.Shared.Models;

using System.Text.Json;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestProject
{
    public class JsonNamingPolicyTests(ITestOutputHelper output)
    {
        [Fact]
        public void Test1()
        {
            var DbwObject = new DbwData()
            {
                IdZmienna = 305,
                IdPrzekroj = 739,
                IdOkres = 247,
                IdSposobPrezentacjiMiara = 5,
                IdDaty = 2020,
                IdWymiar2 = 565,
                IdPozycja2 = 6656078
            };
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new JsonKebabCasePolicy()
            };
            var json = JsonSerializer.Serialize(DbwObject, options);

            output.WriteLine(json);
        }
    }
}