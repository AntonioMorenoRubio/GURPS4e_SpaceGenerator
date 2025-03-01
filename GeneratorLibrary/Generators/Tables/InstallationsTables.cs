using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public static class InstallationsTables
    {
        public static List<SpaceportClass> DetermineSpaceportClasses(int populationRating, int[] rolls)
        {
            if (rolls.Length > 5 || rolls.Length < 5)
                throw new ArgumentOutOfRangeException(nameof(rolls), $"Five rolls are needed to generate spaceports. There are {rolls.Length} rolls.");
            if (populationRating < 0)
                throw new ArgumentOutOfRangeException(nameof(populationRating), "Population Rating cannot be negative.");
            if (rolls.Any(x => x < 3 || x  > 18))
                throw new ArgumentOutOfRangeException(nameof(rolls), "The Rolls must be between 3 and 18.");

            List<SpaceportClass> spaceports = new();

            if (populationRating >= 6 && rolls[0] <= populationRating + 2)
                spaceports.Add(SpaceportClass.V); // Full Facilities

            if (populationRating >= 6 && rolls[1] <= populationRating + 5)
                spaceports.Add(SpaceportClass.IV); // Standard Facilities

            if (rolls[2] <= populationRating + 8)
                spaceports.Add(SpaceportClass.III); // Local Facilities

            if (rolls[3] <= populationRating + 7)
                spaceports.Add(SpaceportClass.II); // Frontier Facilities

            if (rolls[4] <= 14)
                spaceports.Add(SpaceportClass.I); // Emergency Facilities

            if (spaceports.Count is 0)
                spaceports.Add(SpaceportClass.Ø);

            return spaceports;
        }
    }
}
