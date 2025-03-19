namespace GeneratorLibrary.Generators.Tables.Advanced
{
    public static class NumberOfStarsTable
    {
        public static int GenerateNumberOfStars(int roll)
        {
            if (roll < 3)
                throw new ArgumentOutOfRangeException($"La tirada {roll} no es válida para generar el número de estrellas.");

            return roll switch
            {
                <= 10 => 1,
                >= 11 and <= 15 => 2,
                >= 16 => 3
            };
        }
    }
}
