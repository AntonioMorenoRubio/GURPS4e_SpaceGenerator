using System.Globalization;
using System.Text;
using GeneratorLibrary.Generators.Tables;

namespace GeneratorLibrary.Models
{
    public class World
    {
        //Physical Parameters
        public WorldType? Type { get; set; } = null;
        public Atmosphere? Atmosphere { get; set; } = null;
        public HydrographicCoverage? HydrographicCoverage { get; set; } = null;
        public Climate? Climate { get; set; } = null;
        public Characteristics? Characteristics { get; set; } = null;
        public ResourcesHabitability? ResourcesHabitability { get; set; } = null;

        //Social Parameters
        public SettlementData? SettlementData { get; set; } = null;
        public TechLevel? TechLevel { get; set; } = null;
        public Population? Population { get; set; } = null;
        public Society? Society { get; set; } = null;
        public ControlRating? ControlRating { get; set; } = null;
        public Economics? Economics { get; set; } = null;
        public Installations? Installations { get; set; } = null;

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
                sb.AppendLine($"Affinity: {ResourcesHabitability.Affinity}");
            }

            //STEP 8: Settlement Type (SettlementData)
            sb.AppendLine($"Settlement Data:");
            sb.AppendLine($"Settlement Type: {SettlementData?.Type}");
            sb.AppendLine($"Is within claimed Space?: {SettlementData?.IsInClaimedSpace}");

            //STEP 9: Tech Level
            sb.AppendLine($"Tech Level:");
            sb.AppendLine($"TL: {TechLevel?.TL}");
            switch (TechLevel?.Status)
            {
                case TechStatus.StandardMinus3:
                    sb.AppendLine($"Tech Status: Standard - 3");
                    break;
                case TechStatus.StandardMinus2:
                    sb.AppendLine($"Tech Status: Standard - 2");
                    break;
                case TechStatus.StandardMinus1:
                    sb.AppendLine($"Tech Status: Standard - 1");
                    break;
                case TechStatus.Advanced or TechStatus.Delayed:
                    sb.AppendLine($"Tech Status: Standard ({TechLevel?.Status})");
                    break;
                default:
                    sb.AppendLine($"Tech Status: {TechLevel?.Status}");
                    break;
            }

            //STEP 10: Population
            sb.AppendLine("Population:");
            sb.AppendLine($"Carrying Capacity: {Population?.CarryingCapacity.ToString("N0", CultureInfo.CurrentCulture)}");
            sb.AppendLine($"Current Population: {Population?.CurrentPopulation.ToString("N0", CultureInfo.CurrentCulture)}");
            sb.AppendLine($"Population Rating (PR): {Population?.PopulationRating}");

            //STEP 11: Society Type
            if (Society is not null)
            {
                sb.AppendLine("Society:");
                sb.AppendLine($"World Unity: {Society.Unity}");
                sb.AppendLine($"Interstellar Society Type: {Society.InterstellarSocietyType}");
                if (Society.SpecialSocieties.Count > 0)
                {
                    sb.AppendLine($"Society Type: {Society.SocietyType} ({string.Join(',', Society.SpecialSocieties)})");
                }
                else
                    sb.AppendLine($"Society Type: {Society.SocietyType}");
            }

            //STEP 12: Control Rating
            if (ControlRating is not null)
            {
                sb.AppendLine("Control Rating:");
                sb.Append("Control Rating List:");
                sb.AppendLine($"{string.Join(',', ControlRating.CRList)}");
                sb.AppendLine($"Minimum and Maximum CR: {ControlRating.minMaxCR}");
                sb.AppendLine($"Final CR: {ControlRating.CR}");
            }

            //STEP 13: Economics
            if (Economics is not null)
            {
                sb.AppendLine("Economics:");
                sb.AppendLine($"Base Per-Capita Income: ${Economics.BasePerCapitaIncome.ToString("N2", CultureInfo.CurrentCulture)}");
                sb.AppendLine($"Income Modifiers: {string.Join(';', Economics.IncomeModifiers)}");
                sb.AppendLine($"Final Per-Capita Income: ${Economics.BasePerCapitaIncome.ToString("N2", CultureInfo.CurrentCulture)}");
                sb.AppendLine($"Typical Wealth Level: {Economics.TypicalWealthLevel}");
                sb.AppendLine($"Economic Volume: ${Economics.EconomicVolume.ToString("N2", CultureInfo.CurrentCulture)}");
            }

            //STEP 14: Bases and Installations
            if (Installations is not null)
            {
                sb.AppendLine("Bases and Installations:");
                sb.AppendLine($"Spaceport Class: {Installations.SpaceportClass}");
            }
            return sb.ToString();
        }
    }
}
