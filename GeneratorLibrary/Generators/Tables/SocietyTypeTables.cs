using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public static class SocietyTypeTables
    {
        public static WorldUnity GenerateWorldUnity(int roll, int populationRating)
        {
            int modifier = populationRating switch
            {
                <= 4 => 4,
                5 => 3,
                6 => 2,
                7 => 1,
                _ => 0
            };

            return (roll + modifier) switch
            {
                <= 5 => WorldUnity.Diffuse,
                6 => WorldUnity.Factionalized,
                7 => WorldUnity.Coalition,
                8 => WorldUnity.WorldGovernment_Special,
                _ => WorldUnity.WorldGovernment
            };
        }
    }
}
