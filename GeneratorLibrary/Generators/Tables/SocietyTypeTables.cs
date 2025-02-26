using System.Net.Sockets;
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
    }
}
