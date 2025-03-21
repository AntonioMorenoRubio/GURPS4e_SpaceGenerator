﻿using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Generators.Tables.Basic
{
    public static class AtmosphereTables
    {
        private static readonly HashSet<(WorldSize, WorldSubType)> _NoAtmosphereWorlds =
        [
            (WorldSize.Special, WorldSubType.AsteroidBelt),
            (WorldSize.Tiny, WorldSubType.Ice),
            (WorldSize.Tiny, WorldSubType.Rock),
            (WorldSize.Tiny, WorldSubType.Sulfur),
            (WorldSize.Small, WorldSubType.Hadean),
            (WorldSize.Small, WorldSubType.Rock),
            (WorldSize.Standard, WorldSubType.Hadean),
            (WorldSize.Standard, WorldSubType.Chthonian),
            (WorldSize.Large, WorldSubType.Chthonian)
        ];

        private static readonly Dictionary<(WorldSize, WorldSubType), List<string>> _compositionMap = new()
        {
            { (WorldSize.Small, WorldSubType.Ice), new() { "Nitrogen", "Methane" } },
            { (WorldSize.Standard, WorldSubType.Ammonia), new() { "Nitrogen", "Ammonia", "Methane" } },
            { (WorldSize.Large, WorldSubType.Ammonia), new() { "Helium", "Ammonia", "Methane" } },
            { (WorldSize.Standard, WorldSubType.Ice), new() { "Nitrogen", "Carbon Dioxide" } },
            { (WorldSize.Standard, WorldSubType.Ocean), new() { "Nitrogen", "Carbon Dioxide" } },
            { (WorldSize.Large, WorldSubType.Ice), new() { "Helium", "Nitrogen" } },
            { (WorldSize.Large, WorldSubType.Ocean), new() { "Helium", "Nitrogen" } },
            { (WorldSize.Standard, WorldSubType.Garden), new() { "Nitrogen", "Oxygen" } },
            { (WorldSize.Large, WorldSubType.Garden), new() { "Nitrogen", "Noble gases", "Oxygen" } },
            { (WorldSize.Standard, WorldSubType.Greenhouse), new() { "Carbon Dioxide", "Nitrogen" } },
            { (WorldSize.Large, WorldSubType.Greenhouse), new() { "Carbon Dioxide", "Nitrogen" } },
            { (WorldSize.Special, WorldSubType.GasGiant), new() { "Hydrogen", "Helium" } }
        };

        private static readonly Dictionary<(WorldSize, WorldSubType), List<AtmosphereCharacteristic>> _fixedCharacteristics = new()
        {
            { (WorldSize.Standard, WorldSubType.Ammonia), new() {
                AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.LethallyToxic, AtmosphereCharacteristic.Corrosive }
            },
            { (WorldSize.Standard, WorldSubType.Greenhouse), new() {
                AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.LethallyToxic, AtmosphereCharacteristic.Corrosive }
            },
            { (WorldSize.Large, WorldSubType.Ammonia), new() {
                AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.LethallyToxic, AtmosphereCharacteristic.Corrosive }
            },
            { (WorldSize.Large, WorldSubType.Ice), new() {
                AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.HighlyToxic }
            },
            { (WorldSize.Large, WorldSubType.Ocean), new() {
                AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.HighlyToxic }
            },
            { (WorldSize.Large, WorldSubType.Greenhouse), new() {
                AtmosphereCharacteristic.Suffocating }
            }
        };

        public static double GenerateMass(WorldSize size, WorldSubType subType)
        {
            if (_NoAtmosphereWorlds.Contains((size, subType)))
                return 0;

            double variation = Random.Shared.NextDouble() * 0.1f - 0.05f;
            return Math.Round(DiceRoller.Instance.Roll() / 10.0 + variation, 2);
        }

        public static List<string> GetComposition(WorldSize size, WorldSubType subType) =>
            _compositionMap.TryGetValue((size, subType), out var composition)
            ? composition
            : new List<string>();

        public static List<AtmosphereCharacteristic> GenerateCharacteristics(WorldSize size, WorldSubType subType, int roll)
        {
            if (_fixedCharacteristics.TryGetValue((size, subType), out var characteristics))
            {
                return characteristics;
            }

            var result = new List<AtmosphereCharacteristic>();

            switch (size, subType)
            {
                case (WorldSize.Small, WorldSubType.Ice):
                    result.Add(AtmosphereCharacteristic.Suffocating);
                    result.Add(roll <= 15 ? AtmosphereCharacteristic.MildlyToxic : AtmosphereCharacteristic.HighlyToxic);
                    break;

                case (WorldSize.Standard, WorldSubType.Ice):
                case (WorldSize.Standard, WorldSubType.Ocean):
                    result.Add(AtmosphereCharacteristic.Suffocating);
                    if (roll > 12)
                        result.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;

                case (WorldSize.Standard, WorldSubType.Garden):
                case (WorldSize.Large, WorldSubType.Garden):
                    if (roll >= 12)
                        result.Add(AtmosphereCharacteristic.Marginal);
                    break;
            }

            return result;
        }

        public static MarginalAtmosphere GenerateMarginalAtmosphere(int roll) => roll switch
        {
            3 or 4 => MarginalAtmosphere.ChlorineOrFluorine,
            5 or 6 => MarginalAtmosphere.SulphurCompounds,
            7 => MarginalAtmosphere.NitrogenCompounds,
            8 or 9 => MarginalAtmosphere.OrganicToxins,
            10 or 11 => MarginalAtmosphere.LowOxygen,
            12 or 13 => MarginalAtmosphere.Pollutants,
            14 => MarginalAtmosphere.HighCarbonDioxide,
            15 or 16 => MarginalAtmosphere.HighOxygen,
            17 or 18 => MarginalAtmosphere.InertGases,
            _ => throw new ArgumentOutOfRangeException($"Couldn't generate marginal atmosphere. Roll:{roll}.")
        };

        public static Atmosphere? ApplyMarginalAtmosphere(Atmosphere? atmosphere, double randomValue)
        {
            if (atmosphere is null || atmosphere.MarginalAtmosphere is MarginalAtmosphere.None)
                return atmosphere;

            // Clonar la atmósfera original para evitar modificaciones accidentales
            Atmosphere newAtmosphere = new()
            {
                Composition = new List<string>(atmosphere.Composition),
                Characteristics = new List<AtmosphereCharacteristic>(atmosphere.Characteristics),
                MarginalAtmosphere = atmosphere.MarginalAtmosphere,
                Mass = atmosphere.Mass
            };

            switch (atmosphere.MarginalAtmosphere)
            {
                case MarginalAtmosphere.ChlorineOrFluorine:
                    string element = randomValue <= 0.90 ? "Chlorine" : "Fluorine";
                    newAtmosphere.Composition.Add(element);
                    newAtmosphere.Characteristics.Add(AtmosphereCharacteristic.HighlyToxic);
                    break;
                case MarginalAtmosphere.HighCarbonDioxide:
                    newAtmosphere.Composition.Add("Carbon Dioxide");
                    newAtmosphere.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case MarginalAtmosphere.NitrogenCompounds:
                    newAtmosphere.Composition.Add("Nitrogen Oxide");
                    newAtmosphere.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case MarginalAtmosphere.SulphurCompounds:
                    newAtmosphere.Composition.AddRange(["Hydrogen Sulfide", "Sulfur Dioxide", "Sulfur Trioxide"]);
                    newAtmosphere.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case MarginalAtmosphere.OrganicToxins:
                    newAtmosphere.Composition.Add("Spores");
                    newAtmosphere.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
                case MarginalAtmosphere.Pollutants:
                    newAtmosphere.Characteristics.Add(AtmosphereCharacteristic.MildlyToxic);
                    break;
            }

            return newAtmosphere;
        }

        public static double GenerateAtmosphericPressure(WorldSize size, WorldSubType subType, double atmosphereMass, double surfaceGravity)
        {
            // Mundos sin atmósfera (Vacuum)
            if ((size, subType) is
                (WorldSize.Special, WorldSubType.AsteroidBelt) or
                (WorldSize.Tiny, WorldSubType.Ice) or
                (WorldSize.Tiny, WorldSubType.Rock) or
                (WorldSize.Tiny, WorldSubType.Sulfur) or
                (WorldSize.Small, WorldSubType.Hadean) or
                (WorldSize.Standard, WorldSubType.Hadean))
            {
                return 0.0; // Vacuum
            }

            // Mundos con atmósfera traza (Trace)
            if ((size, subType) is
                (WorldSize.Small, WorldSubType.Rock) or
                (WorldSize.Standard, WorldSubType.Chthonian) or
                (WorldSize.Large, WorldSubType.Chthonian))
            {
                return 0.01; // Trace atmosphere
            }

            // Obtener factor de presión según la tabla
            double pressureFactor = (size, subType) switch
            {
                (WorldSize.Small, WorldSubType.Ice) => 10.0,
                (WorldSize.Standard, WorldSubType.Ammonia) or
                (WorldSize.Standard, WorldSubType.Ice) or
                (WorldSize.Standard, WorldSubType.Ocean) or
                (WorldSize.Standard, WorldSubType.Garden) => 1.0,
                (WorldSize.Standard, WorldSubType.Greenhouse) => 100.0,
                (WorldSize.Large, WorldSubType.Ammonia) or
                (WorldSize.Large, WorldSubType.Ice) or
                (WorldSize.Large, WorldSubType.Ocean) or
                (WorldSize.Large, WorldSubType.Garden) => 5.0,
                (WorldSize.Large, WorldSubType.Greenhouse) => 500.0,
                _ => throw new ArgumentOutOfRangeException($"Invalid world type for atmospheric pressure {(size, subType)}")
            };

            // Calcular presión atmosférica
            double pressure = atmosphereMass * pressureFactor * surfaceGravity;

            return pressure;
        }

        public static PressureCategory GetPressureCategory(double pressure) => pressure switch
        {
            < 0.01f => PressureCategory.Trace,
            >= 0.01f and <= 0.5f => PressureCategory.VeryThin,
            > 0.5f and <= 0.8f => PressureCategory.Thin,
            > 0.8f and <= 1.2f => PressureCategory.Standard,
            > 1.2f and <= 1.5f => PressureCategory.Dense,
            > 1.5f and <= 10f => PressureCategory.VeryDense,
            > 10f => PressureCategory.Superdense,
            _ => throw new ArgumentOutOfRangeException($"Invalid pressure to get atmospheric pressure category: {pressure}")
        };
    }
}
