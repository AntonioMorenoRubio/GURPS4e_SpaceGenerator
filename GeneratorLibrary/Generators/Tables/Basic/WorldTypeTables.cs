using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Generators.Tables.Basic
{
    public static class WorldTypeTables
    {
        public static string GetOverallType(int diceRoll) => diceRoll switch
        {
            <= 0 or >= 19 => throw new ArgumentOutOfRangeException($"Invalid dice roll: {diceRoll}"),
            <= 7 => "Hostile",
            >= 8 and <= 13 => "Barren",
            >= 14 and <= 18 => "Garden"
        };

        public static (WorldSize Size, WorldSubType SubType) GenerateWorldType(string overallType, int diceRoll) => overallType switch
        {
            "Hostile" => GenerateHostileWorld(diceRoll),
            "Barren" => GenerateBarrenWorld(diceRoll),
            "Garden" => GenerateGardenWorld(diceRoll),
            _ => throw new ArgumentOutOfRangeException($"OverallType for World not expected: {overallType}")
        };

        public static (WorldSize Size, WorldSubType SubType) GenerateHostileWorld(int diceRoll) => diceRoll switch
        {
            3 or 4 => (WorldSize.Standard, WorldSubType.Chthonian),
            5 or 6 => (WorldSize.Standard, WorldSubType.Greenhouse),
            >= 7 and <= 9 => (WorldSize.Tiny, WorldSubType.Sulfur),
            >= 10 and <= 12 => (WorldSize.Standard, WorldSubType.Ammonia),
            13 or 14 => (WorldSize.Large, WorldSubType.Ammonia),
            15 or 16 => (WorldSize.Large, WorldSubType.Greenhouse),
            17 or 18 => (WorldSize.Large, WorldSubType.Chthonian),
            _ => throw new ArgumentOutOfRangeException($"Couldn't generate hostile world for dice roll {diceRoll}.")
        };

        public static (WorldSize Size, WorldSubType SubType) GenerateBarrenWorld(int diceRoll) => diceRoll switch
        {
            3 => (WorldSize.Small, WorldSubType.Hadean),
            4 => (WorldSize.Small, WorldSubType.Ice),
            5 or 6 => (WorldSize.Small, WorldSubType.Rock),
            7 or 8 => (WorldSize.Tiny, WorldSubType.Rock),
            9 or 10 => (WorldSize.Tiny, WorldSubType.Ice),
            11 or 12 => (WorldSize.Special, WorldSubType.AsteroidBelt),
            13 or 14 => (WorldSize.Standard, WorldSubType.Ocean),
            15 => (WorldSize.Standard, WorldSubType.Ice),
            16 => (WorldSize.Standard, WorldSubType.Hadean),
            17 => (WorldSize.Large, WorldSubType.Ocean),
            18 => (WorldSize.Large, WorldSubType.Ice),
            _ => throw new ArgumentOutOfRangeException($"Couldn't generate barren world for dice roll {diceRoll}.")
        };

        public static (WorldSize Size, WorldSubType SubType) GenerateGardenWorld(int diceRoll) => diceRoll switch
        {
            >= 3 and <= 16 => (WorldSize.Standard, WorldSubType.Garden),
            >= 17 and <= 18 => (WorldSize.Large, WorldSubType.Garden),
            _ => throw new ArgumentOutOfRangeException($"Couldn't generate garden world for dice roll {diceRoll}.")
        };
    }
}
