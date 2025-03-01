using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public static class InstallationsTables
    {
        public static SpaceportClass DetermineSpaceportClass(int populationRating, int roll)
        {
            if (populationRating < 0)
                throw new ArgumentOutOfRangeException(nameof(populationRating), "Population Rating cannot be negative.");
            if (roll < 3 || roll > 18)
                throw new ArgumentOutOfRangeException(nameof(roll), "Roll must be between 3 and 18.");

            if (populationRating >= 6 && roll <= populationRating + 2)
                return SpaceportClass.V; // Full Facilities

            if (populationRating >= 6 && roll <= populationRating + 5)
                return SpaceportClass.IV; // Standard Facilities

            if (roll <= populationRating + 8)
                return SpaceportClass.III; // Local Facilities

            if (roll <= populationRating + 7)
                return SpaceportClass.II; // Frontier Facilities

            if (roll <= 14)
                return SpaceportClass.I; // Emergency Facilities

            return SpaceportClass.Ø;
        }
    }
}
