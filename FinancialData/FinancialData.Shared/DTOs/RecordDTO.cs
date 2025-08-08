using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.DTOs
{
    public class RecordDTO
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string DataType { get; set; }
        public string Frequency { get; set; }
        public string PresentationType { get; set; }

    }


}
