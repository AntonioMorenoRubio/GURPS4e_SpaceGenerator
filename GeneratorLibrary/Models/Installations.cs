using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorLibrary.Models
{
    public record Installations
    {
        public SpaceportClass SpaceportClass { get; set; }
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
