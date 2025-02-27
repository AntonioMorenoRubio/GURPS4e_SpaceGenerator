using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models
{
    public record Economics
    {
        public decimal BasePerCapitaIncome { get; set; }
        public List<double> IncomeModifiers { get; set; } = new();
    }
}
