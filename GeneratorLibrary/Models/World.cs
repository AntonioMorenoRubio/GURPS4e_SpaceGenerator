using System.Text;
using GeneratorLibrary.Generators.Tables;

namespace GeneratorLibrary.Models
{
    public class World
    {
        public WorldType? Type { get; set; } = null;
        public Atmosphere? Atmosphere { get; set; } = null;
        public HydrographicCoverage? HydrographicCoverage { get; set; } = null;
        public Climate? Climate { get; set; } = null;
        public Characteristics? Characteristics { get; set; } = null;
        public ResourcesHabitability? ResourcesHabitability { get; set; } = null;

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

                if (Atmosphere.MarginalAtmosphere is not MarginalAtmosphere.None)
                    sb.AppendLine($"Marginal Atmosphere: {Atmosphere.MarginalAtmosphere}");

                sb.AppendLine($"Pressure: {Atmosphere.Pressure} atm.");
                sb.AppendLine($"Pressure Category: {Atmosphere.PressureCategory}.");
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
            sb.AppendLine($"Original Average Surface Temperature: {Climate?.Kelvin}K, {Climate?.Celsius}ºC, {Climate?.Farenheit}ºF.");
            sb.AppendLine($"Climate Type: {Climate?.ClimateType}");
            sb.AppendLine($"Black body Correction: {Climate?.BlackBodyCorrection}");
            sb.AppendLine($"Black body Temperature: {Climate?.BlackBodyTemperature}K");

            //STEP 6: World Size (Characteristics)
            if (Characteristics is not null)
            {
                sb.AppendLine("World Size:");
                sb.AppendLine($"Density: {Characteristics?.Density} Earth Densities. ({Characteristics?.DensityGCC} g/cc.)");
                sb.AppendLine($"Diameter: {Characteristics?.Diameter} Earth Diameter. {Characteristics?.DiameterKilometers} km. {Characteristics?.DiameterMiles} km.");
                sb.AppendLine($"Surface Gravity: {Characteristics?.SurfaceGravity} Earth Gravities, {Characteristics?.SurfaceDensityInMetresPerSecondSq} m/s^2.");
            }

            //STEP 7: Resources and Habitability
            if (ResourcesHabitability is not null)
            {
                sb.AppendLine("Resources and Habitability:");
                sb.AppendLine($"Resource Value: {ResourcesHabitability.ResourceOverall} ({ResourcesHabitability.ResourceValueModifier})");
                sb.AppendLine($"Habitability: {ResourcesHabitability.Habitability}");
            }

            return sb.ToString();
        }
    }
}
