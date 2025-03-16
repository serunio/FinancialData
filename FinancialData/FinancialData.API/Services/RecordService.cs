using FinancialData.API.Data;
using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialData.API.Services
{
    public class RecordService(FinancialDataContext context)
    {
        private readonly FinancialDataContext _context = context;

        public async Task<List<Record>> GetRecords(GetRecordsDto getRecordsDto)
        {
            var records = await _context.Record
                .Where(r => r.DataTypeId == getRecordsDto.DataTypeId)
                .Where(r => r.FrequencyId == getRecordsDto.FrequencyId)
                .Where(r => r.PresentationTypeId == getRecordsDto.PresentationTypeId)
                .Where(r => r.Date >= DateTime.Parse(getRecordsDto.StartDate ?? "1900-01"))
                .Where(r => r.Date <= DateTime.Parse(getRecordsDto.EndDate ?? "2100-01"))
                .OrderBy(r => r.Date)
                .ToListAsync();
            return records;
        }
    }
}
