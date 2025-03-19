using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Generators.Tables.Basic
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

        public static SocietyType DetermineSocietyType(InterstellarSocietyType interstellarSociety, int techLevel, int roll)
        {
            int modifiedRoll = roll + Math.Min(techLevel, 10); // TL11+ tratado como TL10

            return interstellarSociety switch
            {
                InterstellarSocietyType.AnarchyOrAlliance => modifiedRoll switch
                {
                    <= 6 => SocietyType.Anarchy,
                    <= 8 => SocietyType.ClanTribal,
                    9 => SocietyType.Caste,
                    <= 11 => SocietyType.Feudal,
                    12 => SocietyType.Theocracy,
                    <= 15 => SocietyType.Dictatorship,
                    <= 18 => SocietyType.RepresentativeDemocracy,
                    <= 20 => SocietyType.AthenianDemocracy,
                    <= 22 => SocietyType.CorporateState,
                    <= 25 => SocietyType.Technocracy,
                    <= 27 => SocietyType.Caste,
                    _ => SocietyType.Anarchy
                },

                InterstellarSocietyType.Federation => modifiedRoll switch
                {
                    <= 6 => SocietyType.Anarchy,
                    <= 8 => SocietyType.ClanTribal,
                    9 => SocietyType.Caste,
                    10 => SocietyType.Feudal,
                    11 => SocietyType.Theocracy,
                    <= 14 => SocietyType.Dictatorship,
                    <= 19 => SocietyType.RepresentativeDemocracy,
                    <= 22 => SocietyType.AthenianDemocracy,
                    23 => SocietyType.CorporateState,
                    <= 25 => SocietyType.Technocracy,
                    <= 27 => SocietyType.Caste,
                    _ => SocietyType.Anarchy
                },

                InterstellarSocietyType.CorporateState => modifiedRoll switch
                {
                    <= 6 => SocietyType.Anarchy,
                    <= 8 => SocietyType.ClanTribal,
                    9 => SocietyType.Caste,
                    10 => SocietyType.Theocracy,
                    <= 12 => SocietyType.Feudal,
                    <= 15 => SocietyType.Dictatorship,
                    <= 17 => SocietyType.RepresentativeDemocracy,
                    18 => SocietyType.AthenianDemocracy,
                    <= 21 => SocietyType.CorporateState,
                    <= 25 => SocietyType.Technocracy,
                    <= 27 => SocietyType.Caste,
                    _ => SocietyType.Anarchy
                },

                InterstellarSocietyType.Empire => modifiedRoll switch
                {
                    <= 6 => SocietyType.Anarchy,
                    <= 8 => SocietyType.ClanTribal,
                    9 => SocietyType.Caste,
                    <= 12 => SocietyType.Feudal,
                    13 => SocietyType.Theocracy,
                    <= 17 => SocietyType.Dictatorship,
                    <= 19 => SocietyType.RepresentativeDemocracy,
                    <= 22 => SocietyType.CorporateState,
                    <= 25 => SocietyType.Technocracy,
                    <= 27 => SocietyType.Caste,
                    _ => SocietyType.Anarchy
                },

                _ => throw new ArgumentOutOfRangeException($"No rule for {interstellarSociety} with roll {modifiedRoll}.")
            };
        }

        public static SpecialSociety GenerateSpecialSociety(int roll, bool isMatriarchy) => roll switch
        {
            3 or 4 or 5 => SpecialSociety.Subjugated,
            6 => SpecialSociety.Sanctuary,
            7 or 8 => SpecialSociety.MilitaryGovernment,
            9 => SpecialSociety.Socialist,
            10 => SpecialSociety.Bureaucracy,
            11 or 12 => SpecialSociety.Colony,
            13 or 14 => SpecialSociety.Oligarchy,
            15 => SpecialSociety.Meritocracy,
            16 => isMatriarchy ? SpecialSociety.Matriarchy : SpecialSociety.Patriarchy,
            17 => SpecialSociety.Utopia,
            18 => SpecialSociety.Cybercracy,
            _ => throw new ArgumentOutOfRangeException($"No rule for Special Society with roll {roll}.")
        };
    }
}
