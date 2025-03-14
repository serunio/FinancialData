using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int DataTypeId { get; set; }
        public required DataType DataType { get; set; }
        public int FrequencyId { get; set; }
        public required Frequency Frequency { get; set; }
        public int PresentationTypeId { get; set; }
        public required PresentationType PresentationType { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

    }
}
