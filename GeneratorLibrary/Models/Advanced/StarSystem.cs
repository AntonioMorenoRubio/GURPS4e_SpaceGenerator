using System.Text;

namespace GeneratorLibrary.Models.Advanced
{
    public record StarSystem
    {
        public List<Star> Stars { get; set; } = new();

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

            return sb.ToString();
        }
    }
}
