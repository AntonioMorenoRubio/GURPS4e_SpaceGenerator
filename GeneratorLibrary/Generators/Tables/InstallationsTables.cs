using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public static class InstallationsTables
    {
        public static List<SpaceportClass> DetermineSpaceportClasses(int populationRating, int[] rolls)
        {
            if (rolls.Length > 5 || rolls.Length < 5)
                throw new ArgumentOutOfRangeException(nameof(rolls), $"Five rolls are needed to generate spaceports. There are {rolls.Length} rolls.");
            if (populationRating < 0)
                throw new ArgumentOutOfRangeException(nameof(populationRating), "Population Rating cannot be negative.");
            if (rolls.Any(x => x < 3 || x  > 18))
                throw new ArgumentOutOfRangeException(nameof(rolls), "The Rolls must be between 3 and 18.");

            List<SpaceportClass> spaceports = new();

            if (populationRating >= 6 && rolls[0] <= populationRating + 2)
                spaceports.Add(SpaceportClass.V); // Full Facilities

            if (populationRating >= 6 && rolls[1] <= populationRating + 5)
                spaceports.Add(SpaceportClass.IV); // Standard Facilities

            if (rolls[2] <= populationRating + 8)
                spaceports.Add(SpaceportClass.III); // Local Facilities

            if (rolls[3] <= populationRating + 7)
                spaceports.Add(SpaceportClass.II); // Frontier Facilities

            if (rolls[4] <= 14)
                spaceports.Add(SpaceportClass.I); // Emergency Facilities

            if (spaceports.Count is 0)
                spaceports.Add(SpaceportClass.Ø);

            return spaceports;
        }

        public static List<Facility> GenerateFacilities(int populationRating, int controlRating, int techLevel, List<SpaceportClass> spaceports)
        {
            List<Facility> facilities = new();

            bool ShouldAddFacility(int roll, int target) => roll <= target;

            void TryAddFacility(FacilityType type, int roll, int target, int? pr = null)
            {
                if (ShouldAddFacility(roll, target))
                    facilities.Add(new Facility(type, pr));
            }

            // 🌍 Instalaciones Sociales y Criminales
            TryAddFacility(FacilityType.AlienEnclave, DiceRoller.Instance.Roll(), 6);
            TryAddFacility(FacilityType.BlackMarket, DiceRoller.Instance.Roll(), 9 - controlRating);
            TryAddFacility(FacilityType.CriminalBase, DiceRoller.Instance.Roll(), populationRating + 3, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.RebelOrTerroristBase, DiceRoller.Instance.Roll(), 9, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.RefugeeCamp, DiceRoller.Instance.Roll(), populationRating - 3, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));

            // 🏛️ Instalaciones Políticas y Corporativas
            if (populationRating >= 3)
                TryAddFacility(FacilityType.ColonialOffice, DiceRoller.Instance.Roll(), populationRating + 4);
            if (populationRating >= 6 && techLevel >= 7)
                TryAddFacility(FacilityType.CorporateHQ, DiceRoller.Instance.Roll(), populationRating + 3, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));

            // 🔍 Espionaje y Seguridad
            TryAddFacility(FacilityType.EspionageFacility, DiceRoller.Instance.Roll(), populationRating + 6, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.PirateBase, DiceRoller.Instance.Roll(), 8 - controlRating, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.SpecialJusticeGroupOffice, DiceRoller.Instance.Roll(), populationRating, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));

            // 🧪 Ciencia e Investigación
            TryAddFacility(FacilityType.GovernmentResearchStation, DiceRoller.Instance.Roll(), 12, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.PrivateResearchCenter, DiceRoller.Instance.Roll(), populationRating + 4, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.University, DiceRoller.Instance.Roll(), populationRating - 6, GetUniversityPR(DiceRoller.Instance.Roll()));

            // ⚔️ Bases Militares
            int roll = DiceRoller.Instance.Roll();
            if (spaceports.Contains(SpaceportClass.V) || roll <= populationRating + 3)
                TryAddFacility(FacilityType.NavalBase, roll, populationRating + 3, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));

            roll = DiceRoller.Instance.Roll();
            if (spaceports.Contains(SpaceportClass.IV) || spaceports.Contains(SpaceportClass.V) || roll <= populationRating + 4)
                TryAddFacility(FacilityType.PatrolBase, roll, populationRating + 4, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));

            TryAddFacility(FacilityType.MercenaryBase, DiceRoller.Instance.Roll(), populationRating + 3, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.SurveyBase, DiceRoller.Instance.Roll(), populationRating + 3, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));

            // 🏝️ Otras Instalaciones
            TryAddFacility(FacilityType.NaturePreserve, DiceRoller.Instance.Roll(), 12 - populationRating);
            TryAddFacility(FacilityType.Prison, DiceRoller.Instance.Roll(), 10 - populationRating, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));
            TryAddFacility(FacilityType.ReligiousCenter, DiceRoller.Instance.Roll(), populationRating - 3, GetFacilityPR(populationRating, DiceRoller.Instance.Roll()));

            return facilities;
        }


        private static int GetFacilityPR(int worldPR, int roll)
        {
            return Math.Clamp(roll - 3, 1, worldPR);
        }

        private static int GetUniversityPR(int roll)
        {
            return roll switch
            {
                1 or 2 => 3,
                3 or 4 => 4,
                _ => 5,
            };
        }
    }
}
