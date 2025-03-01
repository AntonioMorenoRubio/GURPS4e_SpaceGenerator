using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models
{
    public record Installations
    {
        public List<SpaceportClass> Spaceports { get; set; } = new();
    }

    public enum SpaceportClass
    {
        Ø,
        I,
        II,
        III,
        IV,
        V
    }
}
