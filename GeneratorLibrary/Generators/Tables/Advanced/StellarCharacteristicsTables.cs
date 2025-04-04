using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Generators.Tables.Advanced
{
    public class StellarEvolutionData
    {
        public string Type { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public double LuminosityMin { get; set; }
        public double? LuminosityMax { get; set; }
        public double? MainSequenceSpan { get; set; }
        public double? SubGiantSpan { get; set; }
        public double? GiantSpan { get; set; }
    }

    public static class StellarCharacteristicsTables
    {
        public const double WhiteDwarfLuminosity = 0.001;

        private static Dictionary<double, StellarEvolutionData> _stellarData = new Dictionary<double, StellarEvolutionData>
        {
            { 0.10, new StellarEvolutionData { Type = "M7", Temperature = 3100, LuminosityMin = 0.0012, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            { 0.15, new StellarEvolutionData { Type = "M6", Temperature = 3200, LuminosityMin = 0.0036, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            { 0.20, new StellarEvolutionData { Type = "M5", Temperature = 3200, LuminosityMin = 0.0079, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            { 0.25, new StellarEvolutionData { Type = "M4", Temperature = 3300, LuminosityMin = 0.015, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            { 0.30, new StellarEvolutionData { Type = "M3", Temperature = 3300, LuminosityMin = 0.024, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            { 0.35, new StellarEvolutionData { Type = "M3", Temperature = 3400, LuminosityMin = 0.037, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            { 0.40, new StellarEvolutionData { Type = "M2", Temperature = 3500, LuminosityMin = 0.054, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            { 0.45, new StellarEvolutionData { Type = "M1", Temperature = 3600, LuminosityMin = 0.07, LuminosityMax = 0.08, MainSequenceSpan = 70, SubGiantSpan = null, GiantSpan = null } },
            { 0.50, new StellarEvolutionData { Type = "M0", Temperature = 3800, LuminosityMin = 0.09, LuminosityMax = 0.11, MainSequenceSpan = 59, SubGiantSpan = null, GiantSpan = null } },
            { 0.55, new StellarEvolutionData { Type = "K8", Temperature = 4000, LuminosityMin = 0.11, LuminosityMax = 0.15, MainSequenceSpan = 50, SubGiantSpan = null, GiantSpan = null } },
            { 0.60, new StellarEvolutionData { Type = "K6", Temperature = 4200, LuminosityMin = 0.13, LuminosityMax = 0.20, MainSequenceSpan = 42, SubGiantSpan = null, GiantSpan = null } },
            { 0.65, new StellarEvolutionData { Type = "K5", Temperature = 4400, LuminosityMin = 0.15, LuminosityMax = 0.25, MainSequenceSpan = 37, SubGiantSpan = null, GiantSpan = null } },
            { 0.70, new StellarEvolutionData { Type = "K4", Temperature = 4600, LuminosityMin = 0.19, LuminosityMax = 0.35, MainSequenceSpan = 30, SubGiantSpan = null, GiantSpan = null } },
            { 0.75, new StellarEvolutionData { Type = "K2", Temperature = 4900, LuminosityMin = 0.23, LuminosityMax = 0.48, MainSequenceSpan = 24, SubGiantSpan = null, GiantSpan = null } },
            { 0.80, new StellarEvolutionData { Type = "K0", Temperature = 5200, LuminosityMin = 0.28, LuminosityMax = 0.65, MainSequenceSpan = 20, SubGiantSpan = null, GiantSpan = null } },
            { 0.85, new StellarEvolutionData { Type = "G8", Temperature = 5400, LuminosityMin = 0.36, LuminosityMax = 0.84, MainSequenceSpan = 17, SubGiantSpan = null, GiantSpan = null } },
            { 0.90, new StellarEvolutionData { Type = "G6", Temperature = 5500, LuminosityMin = 0.45, LuminosityMax = 1.0, MainSequenceSpan = 14, SubGiantSpan = null, GiantSpan = null } },
            { 0.95, new StellarEvolutionData { Type = "G4", Temperature = 5700, LuminosityMin = 0.56, LuminosityMax = 1.3, MainSequenceSpan = 12, SubGiantSpan = 1.8, GiantSpan = 1.1 } },
            { 1.00, new StellarEvolutionData { Type = "G2", Temperature = 5800, LuminosityMin = 0.68, LuminosityMax = 1.6, MainSequenceSpan = 10, SubGiantSpan = 1.6, GiantSpan = 1.0 } },
            { 1.05, new StellarEvolutionData { Type = "G1", Temperature = 5900, LuminosityMin = 0.87, LuminosityMax = 1.9, MainSequenceSpan = 8.8, SubGiantSpan = 1.4, GiantSpan = 0.8 } },
            { 1.10, new StellarEvolutionData { Type = "G0", Temperature = 6000, LuminosityMin = 1.1, LuminosityMax = 2.2, MainSequenceSpan = 7.7, SubGiantSpan = 1.2, GiantSpan = 0.7 } },
            { 1.15, new StellarEvolutionData { Type = "F9", Temperature = 6100, LuminosityMin = 1.4, LuminosityMax = 2.6, MainSequenceSpan = 6.7, SubGiantSpan = 1.0, GiantSpan = 0.6 } },
            { 1.20, new StellarEvolutionData { Type = "F8", Temperature = 6300, LuminosityMin = 1.7, LuminosityMax = 3.0, MainSequenceSpan = 5.9, SubGiantSpan = 0.9, GiantSpan = 0.6 } },
            { 1.25, new StellarEvolutionData { Type = "F7", Temperature = 6400, LuminosityMin = 2.1, LuminosityMax = 3.5, MainSequenceSpan = 5.2, SubGiantSpan = 0.8, GiantSpan = 0.5 } },
            { 1.30, new StellarEvolutionData { Type = "F6", Temperature = 6500, LuminosityMin = 2.5, LuminosityMax = 3.9, MainSequenceSpan = 4.6, SubGiantSpan = 0.7, GiantSpan = 0.4 } },
            { 1.35, new StellarEvolutionData { Type = "F5", Temperature = 6600, LuminosityMin = 3.1, LuminosityMax = 4.5, MainSequenceSpan = 4.1, SubGiantSpan = 0.6, GiantSpan = 0.4 } },
            { 1.40, new StellarEvolutionData { Type = "F4", Temperature = 6700, LuminosityMin = 3.7, LuminosityMax = 5.1, MainSequenceSpan = 3.7, SubGiantSpan = 0.6, GiantSpan = 0.4 } },
            { 1.45, new StellarEvolutionData { Type = "F3", Temperature = 6900, LuminosityMin = 4.3, LuminosityMax = 5.7, MainSequenceSpan = 3.3, SubGiantSpan = 0.5, GiantSpan = 0.3 } },
            { 1.50, new StellarEvolutionData { Type = "F2", Temperature = 7000, LuminosityMin = 5.1, LuminosityMax = 6.5, MainSequenceSpan = 3.0, SubGiantSpan = 0.5, GiantSpan = 0.3 } },
            { 1.60, new StellarEvolutionData { Type = "F0", Temperature = 7300, LuminosityMin = 6.7, LuminosityMax = 8.2, MainSequenceSpan = 2.5, SubGiantSpan = 0.4, GiantSpan = 0.2 } },
            { 1.70, new StellarEvolutionData { Type = "A9", Temperature = 7500, LuminosityMin = 8.6, LuminosityMax = 10, MainSequenceSpan = 2.1, SubGiantSpan = 0.3, GiantSpan = 0.2 } },
            { 1.80, new StellarEvolutionData { Type = "A7", Temperature = 7800, LuminosityMin = 11, LuminosityMax = 13, MainSequenceSpan = 1.8, SubGiantSpan = 0.3, GiantSpan = 0.2 } },
            { 1.90, new StellarEvolutionData { Type = "A6", Temperature = 8000, LuminosityMin = 13, LuminosityMax = 16, MainSequenceSpan = 1.5, SubGiantSpan = 0.2, GiantSpan = 0.1 } },
            { 2.00, new StellarEvolutionData { Type = "A5", Temperature = 8200, LuminosityMin = 16, LuminosityMax = 20, MainSequenceSpan = 1.3, SubGiantSpan = 0.2, GiantSpan = 0.1 } }
        };

        public static StellarEvolutionData GetStellarData(double mass)
        {
            // Si la masa exacta existe en el diccionario, devuélvela
            if (_stellarData.TryGetValue(mass, out StellarEvolutionData? data))
            {
                return data;
            }

            // Si no, encuentra la entrada más cercana
            double closestMass = _stellarData.Keys
                .OrderBy(key => Math.Abs(key - mass))
                .First();

            return _stellarData[closestMass];
        }

        public static string DetermineStarType(double mass) => GetStellarData(mass).Type;

        public static string DetermineLuminosityClass(double mass, double age)
        {
            StellarEvolutionData stellarData = GetStellarData(mass);

            // Si no hay datos de Main Sequence Span, asumimos que es una estrella de secuencia principal de larga vida
            if (!stellarData.MainSequenceSpan.HasValue)
            {
                return "V"; // Clase de luminosidad V (secuencia principal)
            }

            double msSpan = stellarData.MainSequenceSpan.Value;
            double? sgSpan = stellarData.SubGiantSpan;
            double? gSpan = stellarData.GiantSpan;

            if (age <= msSpan)
            {
                return "V"; // Secuencia principal
            }
            else if (sgSpan.HasValue && age <= msSpan + sgSpan.Value)
            {
                return "IV"; // Subgigante
            }
            else if (gSpan.HasValue && age <= msSpan + sgSpan.GetValueOrDefault() + gSpan.Value)
            {
                return "III"; // Gigante
            }
            else
            {
                return "D"; // Enana blanca (remanente estelar)
            }
        }

        public static double DetermineStarRadiusInAu(string luminosityClass, double luminosity, int kelvinTemperature)
        {
            if (luminosityClass == "D")
                return 0.000425;

            return 155_000 * Math.Sqrt(luminosity) * Math.Pow(kelvinTemperature, 2);
        }

        public static double CalculateMainSequenceLuminosity(double mass, double age)
        {
            StellarEvolutionData stellarData = GetStellarData(mass);
            double luminosity;

            // Para estrellas sin datos completos o estrellas de secuencia principal de larga vida
            if (!stellarData.LuminosityMax.HasValue || !stellarData.MainSequenceSpan.HasValue)
            {
                luminosity = stellarData.LuminosityMin * Apply10PercentVariationInEachDirection();
                return luminosity;
            }

            double msSpan = stellarData.MainSequenceSpan.Value;
            double lMin = stellarData.LuminosityMin;
            double lMax = stellarData.LuminosityMax.Value;

            luminosity = (lMin + (age / msSpan) * (lMax - lMin)) * Apply10PercentVariationInEachDirection();

            return luminosity;
        }

        public static int CalculateMainSequenceTemperature(double mass)
        {
            StellarEvolutionData stellarData = GetStellarData(mass);
            return stellarData.Temperature + Random.Shared.Next(-100, 101);
        }

        public static double CalculateSubGiantLuminosity(double mass)
        {
            StellarEvolutionData stellarData = GetStellarData(mass);

            if (!stellarData.LuminosityMax.HasValue)
                throw new ArgumentException($"Star has no maximum luminosity. Cannot be a subgiant. Mass: {mass}");

            return stellarData.LuminosityMax.Value;
        }

        public static int CalculateSubGiantTemperature(double mass, double age)
        {
            StellarEvolutionData stellarData = GetStellarData(mass);

            if (!stellarData.MainSequenceSpan.HasValue)
                throw new ArgumentException($"Star has no main sequence span. Cannot be a subgiant. Mass: {mass}");
            if (!stellarData.SubGiantSpan.HasValue)
                throw new ArgumentException($"Star has no subgiant span. Cannot be a subgiant. Mass: {mass}");

            int temperature = Convert.ToInt32(stellarData.Temperature -
                ((age - stellarData.MainSequenceSpan.Value) / stellarData.SubGiantSpan.Value * (stellarData.Temperature - 4800))
                );

            return temperature + Random.Shared.Next(-100, 101);
        }

        public static int CalculateGiantTemperature(IDiceRoller diceRoller) => diceRoller.Roll(2, -2) * 200 + 3000;

        public static double CalculateGiantLuminosity(double mass)
        {
            StellarEvolutionData stellarData = GetStellarData(mass);
            if (!stellarData.LuminosityMax.HasValue)
                throw new ArgumentException($"Star has no maximum luminosity. Cannot be a giant. Mass: {mass}");

            return 25 * stellarData.LuminosityMax.Value * Apply10PercentVariationInEachDirection();
        }

        public static double WhiteDwarfMass(IDiceRoller diceRoller) => diceRoller.Roll(2, -2) * 0.05 + 0.9;

        public static int WhiteDwarfTemperature(double mass) => GetStellarData(mass).Temperature;

        public static double Apply10PercentVariationInEachDirection()
        {
            return 1 + (Random.Shared.NextDouble() * 0.2 - 0.1);
        }
    }
}
