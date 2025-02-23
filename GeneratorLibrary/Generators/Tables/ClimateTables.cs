using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public static class ClimateTables
    {

        public static float GenerateAverageSurfaceTemperatureInKelvinsByWorldType(WorldSize size, WorldSubType subType)
        {
            // Definir rangos y pasos según el tipo de mundo
            (float min, float max, float step) = (size, subType) switch
            {
                (WorldSize.Special, WorldSubType.AsteroidBelt) => (140f, 500f, 24f),
                (WorldSize.Tiny, WorldSubType.Ice) => (80f, 140f, 4f),
                (WorldSize.Tiny, WorldSubType.Sulfur) => (80f, 140f, 4f),
                (WorldSize.Tiny, WorldSubType.Rock) => (140f, 500f, 24f),
                (WorldSize.Small, WorldSubType.Hadean) => (50f, 80f, 2f),
                (WorldSize.Small, WorldSubType.Ice) => (80f, 140f, 4f),
                (WorldSize.Small, WorldSubType.Rock) => (140f, 500f, 24f),
                (WorldSize.Standard, WorldSubType.Hadean) => (50f, 80f, 2f),
                (WorldSize.Standard, WorldSubType.Ammonia) => (140f, 215f, 5f),
                (WorldSize.Standard, WorldSubType.Ice) => (80f, 230f, 10f),
                (WorldSize.Standard, WorldSubType.Ocean) => (250f, 340f, 6f),
                (WorldSize.Standard, WorldSubType.Garden) => (250f, 340f, 6f),
                (WorldSize.Standard, WorldSubType.Greenhouse) => (500f, 950f, 30f),
                (WorldSize.Standard, WorldSubType.Chthonian) => (500f, 950f, 30f),
                (WorldSize.Large, WorldSubType.Ammonia) => (140f, 215f, 5f),
                (WorldSize.Large, WorldSubType.Ice) => (80f, 230f, 10f),
                (WorldSize.Large, WorldSubType.Ocean) => (250f, 340f, 6f),
                (WorldSize.Large, WorldSubType.Garden) => (250f, 340f, 6f),
                (WorldSize.Large, WorldSubType.Greenhouse) => (500f, 950f, 30f),
                (WorldSize.Large, WorldSubType.Chthonian) => (500f, 950f, 30f),

                _ => throw new ArgumentOutOfRangeException($"No temperature rule for {size} {subType}")
            };

            int baseRoll = DiceRoller.Instance.Roll(3, -3);
            float temperature = min + (baseRoll * step);
            float variation = (Random.Shared.NextSingle() * step) - (step / 2);
            temperature += variation;

            return Math.Clamp(temperature, min, max);
        }
    }
}
