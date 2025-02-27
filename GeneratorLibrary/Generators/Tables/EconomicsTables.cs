namespace GeneratorLibrary.Generators.Tables
{
    public static class EconomicsTables
    {
        public static decimal GetBasePerCapitaIncome(int techLevel) => techLevel switch
        {
            >= 12 => 130_000,
            11 => 97_000,
            10 => 67_000,
            9 => 43_000,
            8 => 31_000,
            7 => 25_000,
            6 => 19_000,
            5 => 13_000,
            4 => 9_600,
            3 => 8_400,
            2 => 8_100,
            1 => 7_800,
            _ => 7_500
        };
    }
}
