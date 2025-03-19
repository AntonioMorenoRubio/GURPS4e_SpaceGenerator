namespace GeneratorLibrary.Models.Basic
{
    public record Society
    {
        public WorldUnity Unity { get; set; }
        public InterstellarSocietyType InterstellarSocietyType { get; set; }
        public SocietyType SocietyType { get; set; }
        public List<SpecialSociety> SpecialSocieties { get; set; } = new();
    }

    public enum WorldUnity
    {
        Diffuse,
        Factionalized,
        Coalition,
        WorldGovernment,
        WorldGovernment_Special
    }

    public enum InterstellarSocietyType
    {
        AnarchyOrAlliance,
        Federation,
        CorporateState,
        Empire
    }

    public enum SocietyType
    {
        Anarchy,
        AthenianDemocracy,
        RepresentativeDemocracy,
        ClanTribal,
        Caste,
        Dictatorship,
        CorporateState,
        Technocracy,
        Theocracy,
        Feudal
    }

    public enum SpecialSociety
    {
        Bureaucracy,
        Colony,
        Cybercracy,
        Matriarchy,
        Meritocracy,
        MilitaryGovernment,
        Oligarchy,
        Patriarchy,
        Sanctuary,
        Socialist,
        Subjugated,
        Utopia
    }
}
