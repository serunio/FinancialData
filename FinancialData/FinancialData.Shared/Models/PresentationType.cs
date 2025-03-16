using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.Models
{
    public class PresentationType : IParam
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public static string Link => "PresentationTypes";
    }
}
