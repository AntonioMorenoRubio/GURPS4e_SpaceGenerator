namespace GeneratorLibrary.Models
{
    public record Economics
    {
        public decimal BasePerCapitaIncome { get; set; }
        public List<decimal> IncomeModifiers { get; set; } = new();
        public decimal FinalPerCapitaIncome { get; set; }
        public WealthLevel TypicalWealthLevel { get; set; }
        public decimal EconomicVolume { get; set; }
    }

    public enum WealthLevel
    {
        DeadBroke,
        Poor,
        Struggling,
        Average,
        Comfortable
    }
}
