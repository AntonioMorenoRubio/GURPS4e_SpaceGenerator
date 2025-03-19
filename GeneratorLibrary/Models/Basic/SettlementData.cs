namespace GeneratorLibrary.Models.Basic
{
    public record SettlementData
    {
        public SettlementType Type { get; set; }
        public bool IsInClaimedSpace { get; set; }
    }

    public enum SettlementType
    {
        Uninhabited,
        Outpost,
        Colony,
        Homeworld
    }
}
