using GeneratorLibrary.Models.Basic;
using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Generators.Tables.Basic
{
    public static class InstallationsTables
    {
        public static List<SpaceportClass> DetermineSpaceportClasses(int populationRating, int[] rolls)
        {
            if (rolls.Length > 5 || rolls.Length < 5)
                throw new ArgumentOutOfRangeException(nameof(rolls), $"Five rolls are needed to generate spaceports. There are {rolls.Length} rolls.");
            if (populationRating < 0)
                throw new ArgumentOutOfRangeException(nameof(populationRating), "Population Rating cannot be negative.");
            if (rolls.Any(x => x < 3 || x > 18))
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

        public static List<Facility> GenerateFacilities(int populationRating, int controlRating, int techLevel, List<SpaceportClass> spaceports, IDiceRoller diceRoller)
        {
            List<Facility> facilities = new();

            CheckAndAddAlienEnclave(facilities, diceRoller);
            CheckAndAddBlackMarket(facilities, controlRating, diceRoller);
            CheckAndAddColonialOffice(facilities, populationRating, diceRoller);
            CheckAndAddCorporateHeadquarters(facilities, populationRating, techLevel, diceRoller);
            CheckAndAddCriminalBase(facilities, populationRating, diceRoller);
            CheckAndAddEspionageFacilities(facilities, populationRating, diceRoller);
            CheckAndAddGovernmentResearchStations(facilities, populationRating, diceRoller);
            CheckAndAddMercenaryBase(facilities, populationRating, diceRoller);
            CheckAndAddNaturePreserve(facilities, populationRating, diceRoller);
            CheckAndAddNavalBase(facilities, populationRating, spaceports, diceRoller);
            CheckAndAddPatrolBase(facilities, populationRating, spaceports, diceRoller);
            CheckAndAddPirateBase(facilities, populationRating, controlRating, diceRoller);
            CheckAndAddPrivateResearchCenters(facilities, populationRating, diceRoller);
            CheckAndAddRebelBase(facilities, populationRating, diceRoller);
            CheckAndAddRefugeeCamps(facilities, populationRating, diceRoller);
            CheckAndAddReligiousCenter(facilities, populationRating, diceRoller);
            CheckAndAddSpecialJusticeGroupOffice(facilities, populationRating, diceRoller);
            CheckAndAddSurveyBase(facilities, populationRating, spaceports, diceRoller);
            CheckAndAddUniversity(facilities, populationRating, diceRoller);
            CheckAndAddPrison(facilities, populationRating, diceRoller);

            ApplyPRRules(facilities, populationRating);

            return facilities;
        }

        public static void CheckAndAddAlienEnclave(List<Facility> facilities, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= 6)
                facilities.Add(new Facility { Type = FacilityType.AlienEnclave });
        }

        public static void CheckAndAddBlackMarket(List<Facility> facilities, int controlRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= 9 - controlRating)
                facilities.Add(new Facility { Type = FacilityType.BlackMarket });
        }

        public static void CheckAndAddColonialOffice(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (populationRating >= 3 && diceRoller.Roll(3, 0) <= populationRating + 4)
                facilities.Add(new Facility { Type = FacilityType.ColonialOffice });
        }

        public static void CheckAndAddCorporateHeadquarters(List<Facility> facilities, int populationRating, int techLevel, IDiceRoller diceRoller)
        {
            if (populationRating >= 6 && techLevel >= 7 && diceRoller.Roll(3, 0) <= populationRating + 3)
            {
                Facility corporateHQ = new Facility { Type = FacilityType.CorporateHeadquarters };
                corporateHQ.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(corporateHQ);
            }
        }

        public static void CheckAndAddCriminalBase(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating + 3)
            {
                Facility criminalBase = new Facility { Type = FacilityType.CriminalBase };
                criminalBase.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(criminalBase);
            }
        }

        public static void CheckAndAddEspionageFacilities(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating + 6)
                facilities.AddRange(AddEspionageFacilities(populationRating, diceRoller));
        }

        public static void CheckAndAddGovernmentResearchStations(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= 12)
            {
                Facility researchStation = new Facility { Type = FacilityType.GovernmentResearchStation };
                researchStation.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -4));

                // Check if secret
                if (diceRoller.Roll(1, 0) <= 2)
                    researchStation.IsSecret = true;

                facilities.Add(researchStation);

                // Check for second research station
                if (diceRoller.Roll(3, 0) <= populationRating)
                {
                    Facility secondStation = new Facility { Type = FacilityType.GovernmentResearchStation };
                    secondStation.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -4));

                    // Check if secret
                    if (diceRoller.Roll(1, 0) <= 2)
                        secondStation.IsSecret = true;

                    facilities.Add(secondStation);
                }
            }
        }

        public static void CheckAndAddMercenaryBase(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating + 3)
            {
                Facility mercenaryBase = new Facility { Type = FacilityType.MercenaryBase };
                mercenaryBase.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(mercenaryBase);
            }
        }

        public static void CheckAndAddNaturePreserve(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= 12 - populationRating)
                facilities.Add(new Facility { Type = FacilityType.NaturePreserve });
        }

        public static void CheckAndAddNavalBase(List<Facility> facilities, int populationRating, List<SpaceportClass> spaceports, IDiceRoller diceRoller)
        {
            if (spaceports.Contains(SpaceportClass.V) || diceRoller.Roll(3, 0) <= populationRating + 3)
            {
                Facility navalBase = new Facility { Type = FacilityType.NavalBase };
                navalBase.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -1));
                facilities.Add(navalBase);
            }
        }

        public static void CheckAndAddPatrolBase(List<Facility> facilities, int populationRating, List<SpaceportClass> spaceports, IDiceRoller diceRoller)
        {
            if (spaceports.Contains(SpaceportClass.IV) || spaceports.Contains(SpaceportClass.V) ||
                diceRoller.Roll(3, 0) <= populationRating + 4)
            {
                Facility patrolBase = new Facility { Type = FacilityType.PatrolBase };
                patrolBase.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -2));
                facilities.Add(patrolBase);
            }
        }

        public static void CheckAndAddPirateBase(List<Facility> facilities, int populationRating, int controlRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= 8 - controlRating)
            {
                Facility pirateBase = new Facility { Type = FacilityType.PirateBase };
                pirateBase.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(pirateBase);
            }
        }

        public static void CheckAndAddPrivateResearchCenters(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating + 4)
                facilities.AddRange(AddPrivateResearchCenters(populationRating, diceRoller));
        }

        public static void CheckAndAddRebelBase(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= 9)
            {
                Facility rebelBase = new Facility { Type = FacilityType.RebelOrTerroristBase };
                rebelBase.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(rebelBase);
            }
        }

        public static void CheckAndAddRefugeeCamps(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating - 3)
                facilities.AddRange(AddRefugeeCamps(populationRating, diceRoller));
        }

        public static void CheckAndAddReligiousCenter(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating - 3)
            {
                Facility religiousCenter = new Facility { Type = FacilityType.ReligiousCenter };
                religiousCenter.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(religiousCenter);
            }
        }

        public static void CheckAndAddSpecialJusticeGroupOffice(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating)
            {
                Facility sjgOffice = new Facility { Type = FacilityType.SpecialJusticeGroupOffice };
                sjgOffice.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -4));

                // Check if covert
                if (diceRoller.Roll(1, 0) <= 2)
                    sjgOffice.IsSecret = true;

                facilities.Add(sjgOffice);
            }
        }

        public static void CheckAndAddSurveyBase(List<Facility> facilities, int populationRating, List<SpaceportClass> spaceports, IDiceRoller diceRoller)
        {
            if (spaceports.Contains(SpaceportClass.IV) || spaceports.Contains(SpaceportClass.V) ||
                diceRoller.Roll(3, 0) <= populationRating + 3)
            {
                Facility surveyBase = new Facility { Type = FacilityType.SurveyBase };
                surveyBase.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(surveyBase);
            }
        }

        public static void CheckAndAddUniversity(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            if (diceRoller.Roll(3, 0) <= populationRating - 6)
            {
                Facility university = new Facility { Type = FacilityType.University };
                university.PR = GetUniversityPR(diceRoller.Roll(1, 0));
                university.PR = Math.Min(university.PR ?? 0, populationRating);
                facilities.Add(university);
            }
        }

        public static void CheckAndAddPrison(List<Facility> facilities, int populationRating, IDiceRoller diceRoller)
        {
            bool canHavePrison = facilities.Count == 0 ||
                                facilities.Count <= 2 &&
                                 facilities.All(f => f.Type == FacilityType.NavalBase || f.Type == FacilityType.PatrolBase);

            if (canHavePrison && diceRoller.Roll(3, 0) <= 10 - populationRating)
            {
                Facility prison = new Facility { Type = FacilityType.Prison };
                prison.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(prison);
            }
        }

        public static void ApplyPRRules(List<Facility> facilities, int populationRating, Random? random = null)
        {
            // Ensure no installation has PR higher than world's PR
            foreach (Facility facility in facilities.Where(f => f.PR > populationRating))
                facility.PR = populationRating;

            // Ensure at most one installation has PR equal to world's PR
            if (populationRating > 0)
            {
                List<Facility> highPRFacilities = facilities.Where(f => f.PR == populationRating).ToList();
                if (highPRFacilities.Count > 1)
                {
                    Random rng = random ?? new Random();
                    // Keep only one with max PR, reduce others
                    int highPRIndex = rng.Next(highPRFacilities.Count);

                    for (int i = 0; i < highPRFacilities.Count; i++)
                        if (i != highPRIndex)
                            highPRFacilities[i].PR = Math.Max(1, populationRating - 1);
                }
            }
        }

        public static IEnumerable<Facility> AddEspionageFacilities(int populationRating, IDiceRoller diceRoller)
        {
            List<Facility> facilities = new();

            if (populationRating + 6 < 18)
            {
                do
                {
                    EspionageFacility espionageFacility = new EspionageFacility { Type = FacilityType.EspionageFacility };
                    int rollType = diceRoller.Roll(1, 0);

                    espionageFacility.SubType = rollType switch
                    {
                        <= 4 => EspionageFacilityType.Civilian,
                        5 => EspionageFacilityType.FriendlyMilitary,
                        _ => EspionageFacilityType.EnemyMilitary
                    };

                    espionageFacility.PR = GetFacilityPR(
                        populationRating,
                        espionageFacility.SubType == EspionageFacilityType.Civilian
                            ? diceRoller.Roll(1, -4) // PR 1d-4 for civilian
                            : diceRoller.Roll(1, -2)); // PR 1d-2 for military

                    facilities.Add(espionageFacility);
                }
                while (diceRoller.Roll(3, 0) <= populationRating + 6); // Puede haber más de uno
            }
            else
            {
                do
                {
                    EspionageFacility espionageFacility = new EspionageFacility { Type = FacilityType.EspionageFacility };
                    int rollType = diceRoller.Roll(1, 0);

                    espionageFacility.SubType = rollType switch
                    {
                        <= 4 => EspionageFacilityType.Civilian,
                        5 => EspionageFacilityType.FriendlyMilitary,
                        _ => EspionageFacilityType.EnemyMilitary
                    };

                    espionageFacility.PR = GetFacilityPR(
                        populationRating,
                        espionageFacility.SubType == EspionageFacilityType.Civilian
                            ? diceRoller.Roll(1, -4) // PR 1d-4 for civilian
                            : diceRoller.Roll(1, -2)); // PR 1d-2 for military

                    facilities.Add(espionageFacility);
                }
                while (diceRoller.Roll(3, 0) <= 17); // Modificado para evitar bucle infinito
            }

            return facilities;
        }

        public static IEnumerable<Facility> AddPrivateResearchCenters(int populationRating, IDiceRoller diceRoller)
        {
            List<Facility> facilities = new();
            int researchCenterCount = 0;

            do
            {
                Facility privateResearchCenter = new Facility { Type = FacilityType.PrivateResearchCenter };
                privateResearchCenter.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -4));
                facilities.Add(privateResearchCenter);
                researchCenterCount++;
            }
            while (researchCenterCount < 3 && diceRoller.Roll(3, 0) <= populationRating + 4); // Máximo 3 centros

            return facilities;
        }

        public static IEnumerable<Facility> AddRefugeeCamps(int populationRating, IDiceRoller diceRoller)
        {
            List<Facility> facilities = new();

            do
            {
                Facility refugeeCamp = new Facility { Type = FacilityType.RefugeeCamp };
                refugeeCamp.PR = GetFacilityPR(populationRating, diceRoller.Roll(1, -3));
                facilities.Add(refugeeCamp);
            }
            while (diceRoller.Roll(3, 0) <= populationRating - 3); // Puede haber más de uno

            return facilities;
        }

        public static int GetFacilityPR(int populationRating, int roll)
        {
            int result = Math.Max(1, roll);
            return Math.Min(result, populationRating);
        }

        public static int GetUniversityPR(int roll) => roll switch
        {
            <= 2 => 3,
            <= 4 => 4,
            _ => 5
        };

        public static bool IsFacilityWithPR(FacilityType facilityType)
        {
            return facilityType == FacilityType.CorporateHeadquarters ||
                   facilityType == FacilityType.CriminalBase ||
                   facilityType == FacilityType.EspionageFacility ||
                   facilityType == FacilityType.GovernmentResearchStation ||
                   facilityType == FacilityType.MercenaryBase ||
                   facilityType == FacilityType.NavalBase ||
                   facilityType == FacilityType.PatrolBase ||
                   facilityType == FacilityType.PirateBase ||
                   facilityType == FacilityType.Prison ||
                   facilityType == FacilityType.PrivateResearchCenter ||
                   facilityType == FacilityType.RebelOrTerroristBase ||
                   facilityType == FacilityType.RefugeeCamp ||
                   facilityType == FacilityType.ReligiousCenter ||
                   facilityType == FacilityType.SpecialJusticeGroupOffice ||
                   facilityType == FacilityType.SurveyBase ||
                   facilityType == FacilityType.University;
        }
    }
}
