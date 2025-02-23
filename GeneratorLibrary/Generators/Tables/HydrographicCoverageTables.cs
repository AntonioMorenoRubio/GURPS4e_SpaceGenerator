using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public class HydrographicCoverageTables
    {
        public static float GenerateHydrographicCoverage(WorldSize size, WorldSubType subType)
        {
            // Definir límites de cobertura según el tipo de mundo
            (float min, float max) = (size, subType) switch
            {
                (WorldSize.Special, WorldSubType.AsteroidBelt) => (0f, 0f),
                (WorldSize.Tiny, WorldSubType.Rock) => (0f, 0f),
                (WorldSize.Small, WorldSubType.Rock) => (0f, 0f),
                (WorldSize.Tiny, WorldSubType.Ice) => (0f, 0f),
                (WorldSize.Small, WorldSubType.Hadean) => (0f, 0f),
                (WorldSize.Standard, WorldSubType.Hadean) => (0f, 0f),
                (WorldSize.Tiny, WorldSubType.Sulfur) => (0f, 0f),
                (WorldSize.Standard, WorldSubType.Chthonian) => (0f, 0f),
                (WorldSize.Large, WorldSubType.Chthonian) => (0f, 0f),

                (WorldSize.Small, WorldSubType.Ice) => (30f, 80f),
                (WorldSize.Standard, WorldSubType.Ammonia) => (50f, 100f),
                (WorldSize.Large, WorldSubType.Ammonia) => (50f, 100f),
                (WorldSize.Standard, WorldSubType.Ice) => (0f, 20f),
                (WorldSize.Large, WorldSubType.Ice) => (0f, 20f),
                (WorldSize.Standard, WorldSubType.Ocean) => (50f, 100f),
                (WorldSize.Standard, WorldSubType.Garden) => (50f, 100f),
                (WorldSize.Large, WorldSubType.Ocean) => (50f, 100f),
                (WorldSize.Large, WorldSubType.Garden) => (50f, 100f),
                (WorldSize.Standard, WorldSubType.Greenhouse) => (0f, 50f),
                (WorldSize.Large, WorldSubType.Greenhouse) => (0f, 50f),

                _ => throw new ArgumentOutOfRangeException($"No hydrographic coverage rule for {size} {subType}")
            };

            // Determinar la cobertura base con los valores de dados
            float coverage = (size, subType) switch
            {
                (WorldSize.Small, WorldSubType.Ice) => (DiceRoller.Instance.Roll(1, 2) * 10),
                (WorldSize.Standard, WorldSubType.Ammonia) => (DiceRoller.Instance.Roll(2) * 10),
                (WorldSize.Large, WorldSubType.Ammonia) => (DiceRoller.Instance.Roll(2) * 10),
                (WorldSize.Standard, WorldSubType.Ice) => (DiceRoller.Instance.Roll(2, -10) * 10),
                (WorldSize.Large, WorldSubType.Ice) => (DiceRoller.Instance.Roll(2, -10) * 10),
                (WorldSize.Standard, WorldSubType.Ocean) => (DiceRoller.Instance.Roll(1, 4) * 10),
                (WorldSize.Standard, WorldSubType.Garden) => (DiceRoller.Instance.Roll(1, 4) * 10),
                (WorldSize.Large, WorldSubType.Ocean) => (DiceRoller.Instance.Roll(1, 6) * 10),
                (WorldSize.Large, WorldSubType.Garden) => (DiceRoller.Instance.Roll(1, 6) * 10),
                (WorldSize.Standard, WorldSubType.Greenhouse) => (DiceRoller.Instance.Roll(2, -7) * 10),
                (WorldSize.Large, WorldSubType.Greenhouse) => (DiceRoller.Instance.Roll(2, -7) * 10),

                _ => 0f // Los mundos con cobertura fija en 0% ya fueron filtrados en la primera switch
            };

            // Aplicar variación de ±5% correctamente
            float variation = Random.Shared.NextSingle() * 0.1f - 0.05f; // Genera valores entre -0.05 y +0.05
            coverage += variation * 100f; // Convertimos la variación a porcentaje

            // Clampear el resultado dentro del rango específico del mundo
            return Math.Clamp(coverage, min, max);
        }

        public static List<string> GetHydrographicComposition(WorldSize size, WorldSubType subType)
        {
            return (size, subType) switch
            {
                // Sin hidrografía
                (WorldSize.Special, WorldSubType.AsteroidBelt) => new(),
                (WorldSize.Tiny, WorldSubType.Rock) => new(),
                (WorldSize.Small, WorldSubType.Rock) => new(),
                (WorldSize.Tiny, WorldSubType.Ice) => new(),
                (WorldSize.Small, WorldSubType.Hadean) => new(),
                (WorldSize.Standard, WorldSubType.Hadean) => new(),
                (WorldSize.Tiny, WorldSubType.Sulfur) => new(),


                // Small Ice → Hidrocarburos líquidos
                (WorldSize.Small, WorldSubType.Ice) => new() { "Liquid Hydrocarbons" },

                // Standard y Large Ammonia → Océanos de amoníaco y agua
                (WorldSize.Standard, WorldSubType.Ammonia) => new() { "Ammonia", "Water" },
                (WorldSize.Large, WorldSubType.Ammonia) => new() { "Ammonia", "Water" },

                // Standard y Large Ice → Agua con impurezas o lagos estacionales
                (WorldSize.Standard, WorldSubType.Ice) => new() { "Water", "Salts", "Seasonal Lakes" },
                (WorldSize.Large, WorldSubType.Ice) => new() { "Water", "Salts", "Seasonal Lakes" },

                // Ocean y Garden → Agua líquida
                (WorldSize.Standard, WorldSubType.Ocean) => new() { "Liquid Water" },
                (WorldSize.Standard, WorldSubType.Garden) => new() { "Liquid Water" },
                (WorldSize.Large, WorldSubType.Ocean) => new() { "Liquid Water" },
                (WorldSize.Large, WorldSubType.Garden) => new() { "Liquid Water" },

                // Greenhouse → Si tienen océanos, están llenos de ácido sulfúrico u otros compuestos tóxicos
                (WorldSize.Standard, WorldSubType.Greenhouse) => new() { "Sulfuric Acid", "Toxic Oceans" },
                (WorldSize.Large, WorldSubType.Greenhouse) => new() { "Sulfuric Acid", "Toxic Oceans" },

                //Chthonian → Posibles ríos y lagos de lava
                (WorldSize.Standard, WorldSubType.Chthonian) => new() { "Possible Lava Lakes and Rivers" },
                (WorldSize.Large, WorldSubType.Chthonian) => new() { "Possible Lava Lakes and Rivers" },

                _ => throw new ArgumentOutOfRangeException($"No hydrographic composition rule for {size} {subType}")
            };
        }

    }
}
