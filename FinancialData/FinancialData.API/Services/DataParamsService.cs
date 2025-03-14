
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

        public async Task<List<Frequency>> GetAllFrequenciesAsync()
        {
            return await _context.Frequency
                .ToListAsync();
        }

        public async Task<List<PresentationType>> GetAllPresentationTypesAsync()
        {
            return await _context.PresentationType
                .ToListAsync();
        }
    }
}
