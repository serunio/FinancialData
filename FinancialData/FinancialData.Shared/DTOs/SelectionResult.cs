using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.DTOs
{
    public class SelectionResult
    {
        public int DataTypeId { get; set; }
        public int FrequencyId { get; set; }
        public int PresentationTypeId { get; set; }
        public string Caption { get; set; }
    }
}
