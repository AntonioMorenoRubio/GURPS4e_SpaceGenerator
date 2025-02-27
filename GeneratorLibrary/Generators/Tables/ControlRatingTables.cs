using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public class ControlRatingTables
    {
        public static (int MinCR, int MaxCR) GetControlRatingRange(SocietyType type) => type switch
        {
            SocietyType.Anarchy => (0, 0),
            SocietyType.AthenianDemocracy => (2, 4),
            SocietyType.RepresentativeDemocracy => (2, 4),
            SocietyType.ClanTribal => (3, 5),
            SocietyType.Caste => (3, 6),
            SocietyType.Dictatorship => (3, 6),
            SocietyType.Technocracy => (3, 6),
            SocietyType.Theocracy => (3, 6),
            SocietyType.CorporateState => (4, 6),
            SocietyType.Feudal => (4, 6),
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"No CR range defined for {type}")
        };

        public static (int MinCR, int MaxCR) GetControlRatingRange(SpecialSociety type) => type switch
        {
            SpecialSociety.Bureaucracy => (4, 6), // CR4+
            SpecialSociety.Colony => (3, 6), // Reduce CR del "mother society" pero sin especificar mínimo
            SpecialSociety.Cybercracy => (3, 6), // CR3+
            SpecialSociety.Matriarchy => (0, 6), // Cualquier CR
            SpecialSociety.Meritocracy => (3, 6), // CR3+
            SpecialSociety.MilitaryGovernment => (4, 6), // CR4+
            SpecialSociety.Oligarchy => (3, 6), // CR3+
            SpecialSociety.Patriarchy => (0, 6), // Cualquier CR
            SpecialSociety.Sanctuary => (0, 4), // Rara vez >4
            SpecialSociety.Socialist => (3, 6), // CR3+
            SpecialSociety.Subjugated => (4, 6), // CR4+
            SpecialSociety.Utopia => (0, 3), // CR siempre parece bajo
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"No CR range defined for {type}")
        };

        public static int GenerateControlRatingInRange(int minCR, int maxCR, int roll)
        {
            return (int)Math.Round(minCR + (roll - 2) / 10.0 * (maxCR - minCR));
        }
    }
}
