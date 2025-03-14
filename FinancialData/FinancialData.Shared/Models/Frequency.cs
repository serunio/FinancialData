using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.Models
{
    public class Frequency
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
