using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.DTOs
{
    public class GetRecordsDto
    {
        public int DataTypeId { get; set; }
        public int FrequencyId { get; set; }
        public int PresentationTypeId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}
