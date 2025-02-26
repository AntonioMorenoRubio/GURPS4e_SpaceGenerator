namespace GeneratorLibrary.Models
{
    public record Society
    {
        public WorldUnity Unity { get; set; }
    }

    public enum WorldUnity
    {
        Diffuse,
        Factionalized,
        Coalition,
        WorldGovernment,
        WorldGovernment_Special
    }
}
