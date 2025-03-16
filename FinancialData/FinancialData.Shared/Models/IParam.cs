using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialData.Shared.Models
{
    public interface IParam
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract static string Link { get; }
    }
}
