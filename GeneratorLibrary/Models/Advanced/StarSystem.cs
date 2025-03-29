using System.Text;

namespace GeneratorLibrary.Models.Advanced
{
    public record StarSystem
    {
        public List<Star> Stars { get; set; } = new();
        public StellarAge StellarAge { get; set; } = new();

        public override string ToString()
        {
            if (Stars.Count == 0)
                return string.Empty;

            StringBuilder sb = new();

            //STEP 15: Number of stars
            if (Stars.Count == 1)
                sb.AppendLine("Sistema con 1 estrella.");
            else
                sb.AppendLine($"Sistema con {Stars.Count} estrellas.");

            //STEP 16: Solar Masses
            sb.AppendLine("Masas Solares:");
            sb.AppendLine($"A: {Stars[0].Mass}");
            if (Stars.Count > 1)
                sb.AppendLine($"B: {Stars[1].Mass}");
            if (Stars.Count > 2)
                sb.AppendLine($"C: {Stars[2].Mass}");

            //STEP 17: Stellar Age
            sb.AppendLine("Edad Estelar:");
            sb.AppendLine($"Tipo: {StellarAge.Type}");
            sb.AppendLine($"Edad: {StellarAge.Age} mil millones de años.");

            return sb.ToString();
        }
    }
}
