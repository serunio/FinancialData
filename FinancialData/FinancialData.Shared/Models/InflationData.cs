using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FinancialData.Shared.Models
{
    public class InflationData
    {
        [Key]
        public int Key { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal InflationRate { get; set; }
    }
}
