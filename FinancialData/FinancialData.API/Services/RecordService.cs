using FinancialData.API.Data;
using FinancialData.Shared.DTOs;
using FinancialData.Shared.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
