using System.Text;

namespace GeneratorLibrary.Models
{
    public class World
    {
        public WorldType? Type { get; set; }
        public Atmosphere? Atmosphere { get; set; }
        public HydrographicCoverage? HydrographicCoverage { get; set; }
        public Climate? Climate { get; set; }

        public World() { }

        public override string ToString()
        {
            StringBuilder sb = new();

            //STEP 2: World Type
            sb.AppendLine($"World Type: {Type?.Size} {Type?.SubType}");

            //STEP 3: Atmosphere
            if (Atmosphere is null)
            {
                sb.AppendLine("Atmosphere: None");
            }
            else
            {
                sb.AppendLine($"Atmosphere:");
                sb.AppendLine($"Mass: {Atmosphere.Mass}");
                if (Atmosphere.Composition?.Count > 0)
                {
                    sb.Append("Composition: ");
                    for (int i = 0; i < Atmosphere.Composition.Count; i++)
                    {
                        string compound = Atmosphere.Composition[i];
                        sb.Append($"{compound}, ");
                    }
                    sb.AppendLine("");
                }

                if (Atmosphere.Characteristics?.Count > 0)
                {
                    sb.Append("Characteristics: ");
                    for (int i = 0; i < Atmosphere.Characteristics.Count; i++)
                    {
                        AtmosphereCharacteristic characteristic = Atmosphere.Characteristics[i];
                        sb.Append($"{characteristic}, ");
                    }
                    sb.AppendLine("");
                }

                if (Atmosphere.MarginalAtmosphere is not null)
                    sb.AppendLine($"Marginal Atmosphere: {Atmosphere.MarginalAtmosphere}");
            }

            //STEP 4: Hydrographic Coverage
            sb.AppendLine("Hydrographic Coverage:");
            sb.AppendLine($"Coverage: {HydrographicCoverage?.Coverage.ToString("F2")}%");

            if (HydrographicCoverage?.Composition.Count > 0)
            {
                sb.Append("Composition: ");
                for (int i = 0; i < HydrographicCoverage?.Composition?.Count; i++)
                {
                    sb.Append(HydrographicCoverage.Composition[i]);
                    if (i < HydrographicCoverage.Composition.Count - 1)
                        sb.Append(", ");
                }
            }
            sb.AppendLine();

            //STEP 5: Climate
            sb.AppendLine("Climate:");
            sb.AppendLine($"Original Average Surface Temperature: {Climate?.Kelvin}K, {Climate?.Celsius}�C, {Climate?.Farenheit}�F.");
            sb.AppendLine($"Climate Type: {Climate?.ClimateType}");
            sb.AppendLine($"Black body Correction: {Climate?.BlackBodyCorrection}");
            sb.AppendLine($"Black body Temperature: {Climate?.BlackBodyTemperature}K");

            return sb.ToString();
        }
    }
}
