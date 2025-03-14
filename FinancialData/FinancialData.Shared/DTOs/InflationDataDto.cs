using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.DTOs
{
    public class InflationDataDto
    {
        public DateTime Date {  get; set; }
        public decimal InflationRate { get; set; }
    }
}
