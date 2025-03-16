
using FinancialData.API.Data;
using FinancialData.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialData.API.Services
{
    public class DataParamsService
    {
        private FinancialDataContext _context;

        public DataParamsService(FinancialDataContext context)
        {
            _context = context;
        }

        public async Task<List<DataType>> GetAllDataTypesAsync()
        {
            return await _context.DataType
                .ToListAsync();
        }

        public async Task<List<Frequency>> GetFrequenciesAsync(int DataTypeId = 0, int PresentationTypeId = 0)
        {
            if (DataTypeId == 0)
                return await _context.Frequency
                    .ToListAsync();
            var query = _context.Record
                .Where(r => r.DataTypeId == DataTypeId);
            if (PresentationTypeId != 0)
                query = query.Where(r => r.PresentationTypeId == PresentationTypeId);
            var result = query
                .Select(r => r.FrequencyId)
                .Distinct();
            return await _context.Frequency
                .Where(f => result.Contains(f.Id))
                .ToListAsync();
        }

        public async Task<List<PresentationType>> GetPresentationTypesAsync(int DataTypeId = 0, int FrequencyId = 0)
        {
            if (DataTypeId == 0)
                return await _context.PresentationType
                    .ToListAsync();
            var query = _context.Record
                .Where(r => r.DataTypeId == DataTypeId);
            if (FrequencyId != 0)
                query = query.Where(r => r.FrequencyId == FrequencyId);       
            var result = query
                .Select(r => r.PresentationTypeId)
                .Distinct();
            return await _context.PresentationType
                .Where(t => result.Contains(t.Id))
                .ToListAsync();
        }
    }
}
