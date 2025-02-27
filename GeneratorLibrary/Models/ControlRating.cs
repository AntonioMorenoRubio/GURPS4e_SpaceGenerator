using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models
{
    public record ControlRating
    {
        public int CR { get; set; }
        public (int Min, int Max) minMaxCR { get; set; }
        public List<(int, int)> CRList { get; set; } = new();
    }
}
