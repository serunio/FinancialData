using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using FinancialData.API.Data;
using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static System.Text.RegularExpressions.Regex;
using Exception = System.Exception;

namespace FinancialData.API.Services
{
    public class RecordService(FinancialDataContext context)
    {
        private readonly FinancialDataContext _context = context;

        public async Task<List<Record>> GetRecords(SelectionResult getRecordsDto)
        {
            var records = await _context.Record
                .Where(r => r.DataTypeId == getRecordsDto.DataTypeId)
                .Where(r => r.FrequencyId == getRecordsDto.FrequencyId)
                .Where(r => r.PresentationTypeId == getRecordsDto.PresentationTypeId)
                .OrderBy(r => r.Date)
                .ToListAsync();
            return records;
        }

        public async Task AddRecords(IFormFile file)
        {
            // if (!file.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            //     throw new Exception($"Nieprawidłowy format pliku \"{file.Name}\". Oczekiwano pliku CSV.");

            var firstLineReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
            var headerLine = await firstLineReader.ReadLineAsync();
            if (headerLine == null)
                throw new Exception("Plik jest pusty");

            var hasHeader = !headerLine.Any(char.IsDigit);
            var separator = headerLine.Count(c => c == ',') > headerLine.Count(c => c == ';') ? ',' : ';';
            firstLineReader.Dispose();
            using var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);

            var config = new CsvConfiguration(new CultureInfo("pl-PL"))
            {
                Delimiter = separator.ToString(),
                HasHeaderRecord = hasHeader,
                PrepareHeaderForMatch = args => Replace(args.Header, @"[\s\-_]", "").ToLowerInvariant()
            };
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap(hasHeader ? typeof(RecordByNameMap) : typeof(RecordByIndexMap));
            var records = csv.GetRecords<RecordDTO>().ToList();
            var readyRecords = await MapToRecords(records);
            _context.Record.AddRange(readyRecords);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Record>> MapToRecords(List<RecordDTO> recordsDto)
        {
            var dataTypeDict = await _context.DataType
                .ToDictionaryAsync(dt => dt.Name.ToLowerInvariant(), dt => dt.Id);
            var frequencyDict = await _context.Frequency
                .ToDictionaryAsync(f => f.Name.ToLowerInvariant(), f => f.Id);
            var presentationTypeDict = await _context.PresentationType
                .ToDictionaryAsync(pt => pt.Name.ToLowerInvariant(), pt => pt.Id);

            var newDataTypes = recordsDto.Select(r => r.DataType.ToLowerInvariant()).Distinct()
                .Where(dt => !dataTypeDict.ContainsKey(dt))
                .Select(dt => new DataType { Name = dt })
                .ToList();
            var newPresentationType = recordsDto.Select(r => r.PresentationType.ToLowerInvariant()).Distinct()
                .Where(pt => !presentationTypeDict.ContainsKey(pt))
                .Select(pt => new PresentationType { Name = pt })
                .ToList();
            if(newDataTypes.Any()) _context.DataType.AddRange(newDataTypes);
            if(newPresentationType.Any()) _context.PresentationType.AddRange(newPresentationType);
            await _context.SaveChangesAsync();

            foreach(var dt in newDataTypes)
                dataTypeDict[dt.Name] = dt.Id;
            foreach (var pt in newPresentationType)
                presentationTypeDict[pt.Name] = pt.Id;

            var records = new List<Record>();
            foreach (var recordDto in recordsDto)
            {
                if(!frequencyDict.TryGetValue(recordDto.Frequency.ToLowerInvariant(), out var frequencyId))
                {
                    throw new Exception($"zła częstotliwość \"{recordDto.Frequency.ToLowerInvariant()}\"");
                    continue;
                }

                if (recordDto.Date.Day != 1 ||
                    (frequencyId == 1 && recordDto.Date.Month != 1) || //Yearly
                    (frequencyId == 2 && !new[] { 1, 4, 7, 10 }.Contains(recordDto.Date.Month))) //Quarterly

                {
                    throw new Exception($"zła data \"{recordDto.Date}\"");
                    continue;
                }

                var record = new Record
                {
                    DataTypeId = dataTypeDict[recordDto.DataType.ToLowerInvariant()],
                    FrequencyId = frequencyId,
                    PresentationTypeId = presentationTypeDict[recordDto.PresentationType.ToLowerInvariant()],
                    Date = recordDto.Date,
                    Value = recordDto.Value
                };
                records.Add(record);
            }
            return records;
        }
    }
}
internal sealed class RecordByNameMap : ClassMap<RecordDTO>
{
    public RecordByNameMap()
    {
        Map(m => m.DataType).Name("datatype", "typdanych");
        Map(m => m.Frequency).Name("frequency", "częstotliwość");
        Map(m => m.PresentationType).Name("presentationtype", "sposóbprezentacji");
        Map(m => m.Date).Name("date", "data");
        Map(m => m.Value).Name("value", "wartość");
    }
}

internal sealed class RecordByIndexMap : ClassMap<RecordDTO>
{
    public RecordByIndexMap()
    {
        Map(m => m.DataType).Index(0);
        Map(m => m.Frequency).Index(1);
        Map(m => m.PresentationType).Index(2);
        Map(m => m.Date).Index(3);
        Map(m => m.Value).Index(4);
    }
}
