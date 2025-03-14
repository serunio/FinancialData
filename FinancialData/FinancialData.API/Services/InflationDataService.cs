using System.Data;
using System.Globalization;
using ExcelDataReader;
using FinancialData.API.Data;
using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialData.API.Services
{
    public class InflationDataService
    {
        private FinancialDataContext _context;

        public InflationDataService(FinancialDataContext context)
        {
            _context = context;
        }

        public async Task<List<InflationDataDto>> GetAllInflationDataAsync()
        {
            return await _context.InflationData
                .Select(x => new InflationDataDto
            {
                    Date = x.Date,
                    InflationRate = x.InflationRate
            })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<InflationDataDto>> GetInflationDataByDateAsync(DateTime startDate, DateTime endDate) 
        {
            return await _context.InflationData
                .Select(x => new InflationDataDto 
                {
                    Date = x.Date,
                    InflationRate = x.InflationRate
                })
                .Where(x => x.Date >= startDate && x.Date < endDate)
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task AddInflationDataAsync(InflationDataDto dto)
        {
            _context.InflationData.Add(new InflationData
            {
                Date = dto.Date,
                InflationRate = dto.InflationRate
            });
            await _context.SaveChangesAsync();
        }

        public async Task ClearInflationDataAsync()
        {
            _context.InflationData.RemoveRange(_context.InflationData);
            await _context.SaveChangesAsync();
        }

        public async Task ImportFromExcelAsync(Stream fileStream)
        {
            await ClearInflationDataAsync();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using var reader = ExcelReaderFactory.CreateReader(fileStream);
            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            var table = result.Tables[0];
            table = table.Select("[Sposób prezentacji] = 'Analogiczny miesiąc poprzedniego roku = 100'").CopyToDataTable();

            var inflationDataList = new List<InflationData>();

            for(int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                if (int.TryParse(row["Rok"]?.ToString(), out int year) &&
                    int.TryParse(row["Miesiąc"]?.ToString(), out int month) &&
                    decimal.TryParse(row["Wartość"]?.ToString()?.Replace(',','.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal inflationRate))
                {
                    var date = new DateTime(year, month, 1).Date;
                    inflationDataList.Add(new InflationData
                    {
                        Date = date,
                        InflationRate = inflationRate
                    });
                }
            }
            _context.InflationData.AddRange(inflationDataList);
            await _context.SaveChangesAsync();
        }
    }
}
