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
            return sb.ToString();
        }
    }
}
