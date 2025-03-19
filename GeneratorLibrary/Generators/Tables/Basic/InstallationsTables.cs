using GeneratorLibrary.Models.Basic;

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

        public static List<Facility> GenerateFacilities(int populationRating, int controlRating, int techLevel, List<SpaceportClass> spaceports)
        {
            List<Facility> facilities = new();

            // Alien Enclave
            if (DiceRoller.Instance.Roll(3, 0) <= 6)
            {
                facilities.Add(new Facility { Type = FacilityType.AlienEnclave });
            }

            // Black Market
            if (DiceRoller.Instance.Roll(3, 0) <= 9 - controlRating)
            {
                facilities.Add(new Facility { Type = FacilityType.BlackMarket });
            }

            // Colonial Office
            if (populationRating >= 3 && DiceRoller.Instance.Roll(3, 0) <= populationRating + 4)
            {
                facilities.Add(new Facility { Type = FacilityType.ColonialOffice });
            }

            // Corporate Headquarters
            if (populationRating >= 6 && techLevel >= 7 && DiceRoller.Instance.Roll(3, 0) <= populationRating + 3)
            {
                Facility corporateHQ = new Facility { Type = FacilityType.CorporateHeadquarters };
                corporateHQ.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(corporateHQ);
            }

            // Criminal Base
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating + 3)
            {
                Facility criminalBase = new Facility { Type = FacilityType.CriminalBase };
                criminalBase.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(criminalBase);
            }

            // Espionage Facility
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating + 6)
            {
                facilities.AddRange(AddEspionageFacilities(populationRating));
            }

            // Government Research Station
            if (DiceRoller.Instance.Roll(3, 0) <= 12)
            {
                Facility researchStation = new Facility { Type = FacilityType.GovernmentResearchStation };
                researchStation.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -4));

                // Check if secret
                if (DiceRoller.Instance.Roll(1, 0) <= 2)
                    researchStation.IsSecret = true;

                facilities.Add(researchStation);

                // Check for second research station
                if (DiceRoller.Instance.Roll(3, 0) <= populationRating)
                {
                    Facility secondStation = new Facility { Type = FacilityType.GovernmentResearchStation };
                    secondStation.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -4));

                    // Check if secret
                    if (DiceRoller.Instance.Roll(1, 0) <= 2)
                        secondStation.IsSecret = true;

                    facilities.Add(secondStation);
                }
            }

            // Mercenary Base
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating + 3)
            {
                Facility mercenaryBase = new Facility { Type = FacilityType.MercenaryBase };
                mercenaryBase.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(mercenaryBase);
            }

            // Nature Preserve
            if (DiceRoller.Instance.Roll(3, 0) <= 12 - populationRating)
            {
                facilities.Add(new Facility { Type = FacilityType.NaturePreserve });
            }

            // Naval Base
            if (spaceports.Contains(SpaceportClass.V) || DiceRoller.Instance.Roll(3, 0) <= populationRating + 3)
            {
                Facility navalBase = new Facility { Type = FacilityType.NavalBase };
                navalBase.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -1));
                facilities.Add(navalBase);
            }

            // Patrol Base
            if (spaceports.Contains(SpaceportClass.IV) || spaceports.Contains(SpaceportClass.V) ||
                DiceRoller.Instance.Roll(3, 0) <= populationRating + 4)
            {
                Facility patrolBase = new Facility { Type = FacilityType.PatrolBase };
                patrolBase.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -2));
                facilities.Add(patrolBase);
            }

            // Pirate Base
            if (DiceRoller.Instance.Roll(3, 0) <= 8 - controlRating)
            {
                Facility pirateBase = new Facility { Type = FacilityType.PirateBase };
                pirateBase.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(pirateBase);
            }

            // Prison - Checked last, as it requires no other installations except naval or patrol bases (S98)

            // Private Research Center
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating + 4)
            {
                facilities.AddRange(AddPrivateResearchCenters(populationRating));
            }

            // Rebel or Terrorist Base
            if (DiceRoller.Instance.Roll(3, 0) <= 9)
            {
                Facility rebelBase = new Facility { Type = FacilityType.RebelOrTerroristBase };
                rebelBase.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(rebelBase);
            }

            // Refugee Camp
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating - 3)
            {
                facilities.AddRange(AddRefugeeCamps(populationRating));
            }

            // Religious Center
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating - 3)
            {
                Facility religiousCenter = new Facility { Type = FacilityType.ReligiousCenter };
                religiousCenter.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(religiousCenter);
            }

            // Special Justice Group Office
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating)
            {
                Facility sjgOffice = new Facility { Type = FacilityType.SpecialJusticeGroupOffice };
                sjgOffice.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -4));

                // Check if covert
                if (DiceRoller.Instance.Roll(1, 0) <= 2)
                    sjgOffice.IsSecret = true;

                facilities.Add(sjgOffice);
            }

            // Survey Base
            if (spaceports.Contains(SpaceportClass.IV) || spaceports.Contains(SpaceportClass.V) ||
                DiceRoller.Instance.Roll(3, 0) <= populationRating + 3)
            {
                Facility surveyBase = new Facility { Type = FacilityType.SurveyBase };
                surveyBase.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(surveyBase);
            }

            // University
            if (DiceRoller.Instance.Roll(3, 0) <= populationRating - 6)
            {
                Facility university = new Facility { Type = FacilityType.University };
                university.PR = GetUniversityPR(DiceRoller.Instance.Roll(1, 0));
                university.PR = Math.Min(university.PR ?? 0, populationRating);
                facilities.Add(university);
            }

            // Prison - NOW WE CHECK IF IT CAN BE ADDED
            bool canHavePrison = facilities.Count == 0 ||
                                facilities.Count <= 2 &&
                                 facilities.All(f => f.Type == FacilityType.NavalBase || f.Type == FacilityType.PatrolBase);

            if (canHavePrison && DiceRoller.Instance.Roll(3, 0) <= 10 - populationRating)
            {
                Facility prison = new Facility { Type = FacilityType.Prison };
                prison.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(prison);
            }

            // Ensure no installation has PR higher than world's PR
            foreach (Facility facility in facilities.Where(f => f.PR > populationRating))
                facility.PR = populationRating;

            // Ensure at most one installation has PR equal to world's PR
            if (populationRating > 0)
            {
                List<Facility> highPRFacilities = facilities.Where(f => f.PR == populationRating).ToList();
                if (highPRFacilities.Count > 1)
                {
                    // Keep only one with max PR, reduce others
                    Random random = new Random();
                    int highPRIndex = random.Next(highPRFacilities.Count);

                    for (int i = 0; i < highPRFacilities.Count; i++)
                    {
                        if (i != highPRIndex)
                        {
                            highPRFacilities[i].PR = Math.Max(1, populationRating - 1);
                        }
                    }
                }
            }

            return facilities;
        }

        public static IEnumerable<Facility> AddEspionageFacilities(int populationRating)
        {
            List<Facility> facilities = new();

            if (populationRating + 6 < 18)
            {
                do
                {
                    EspionageFacility espionageFacility = new EspionageFacility { Type = FacilityType.EspionageFacility };
                    int rollType = DiceRoller.Instance.Roll(1, 0);

                    espionageFacility.SubType = rollType switch
                    {
                        <= 4 => EspionageFacilityType.Civilian,
                        5 => EspionageFacilityType.FriendlyMilitary,
                        _ => EspionageFacilityType.EnemyMilitary
                    };

                    espionageFacility.PR = GetFacilityPR(
                        populationRating,
                        espionageFacility.SubType == EspionageFacilityType.Civilian
                            ? DiceRoller.Instance.Roll(1, -4) // PR 1d-4 for civilian
                            : DiceRoller.Instance.Roll(1, -2)); // PR 1d-2 for military

                    facilities.Add(espionageFacility);
                }
                while (DiceRoller.Instance.Roll(3, 0) <= populationRating + 6); // Puede haber más de uno
            }
            else
            {
                do
                {
                    EspionageFacility espionageFacility = new EspionageFacility { Type = FacilityType.EspionageFacility };
                    int rollType = DiceRoller.Instance.Roll(1, 0);

                    espionageFacility.SubType = rollType switch
                    {
                        <= 4 => EspionageFacilityType.Civilian,
                        5 => EspionageFacilityType.FriendlyMilitary,
                        _ => EspionageFacilityType.EnemyMilitary
                    };

                    espionageFacility.PR = GetFacilityPR(
                        populationRating,
                        espionageFacility.SubType == EspionageFacilityType.Civilian
                            ? DiceRoller.Instance.Roll(1, -4) // PR 1d-4 for civilian
                            : DiceRoller.Instance.Roll(1, -2)); // PR 1d-2 for military

                    facilities.Add(espionageFacility);
                }
                while (DiceRoller.Instance.Roll(3, 0) <= 17); // Modificado para evitar bucle infinito
            }

            return facilities;
        }

        public static IEnumerable<Facility> AddPrivateResearchCenters(int populationRating)
        {
            List<Facility> facilities = new();
            int researchCenterCount = 0;

            do
            {
                Facility privateResearchCenter = new Facility { Type = FacilityType.PrivateResearchCenter };
                privateResearchCenter.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -4));
                facilities.Add(privateResearchCenter);
                researchCenterCount++;
            }
            while (researchCenterCount < 3 && DiceRoller.Instance.Roll(3, 0) <= populationRating + 4); // Máximo 3 centros

            return facilities;
        }

        public static IEnumerable<Facility> AddRefugeeCamps(int populationRating)
        {
            List<Facility> facilities = new();

            do
            {
                Facility refugeeCamp = new Facility { Type = FacilityType.RefugeeCamp };
                refugeeCamp.PR = GetFacilityPR(populationRating, DiceRoller.Instance.Roll(1, -3));
                facilities.Add(refugeeCamp);
            }
            while (DiceRoller.Instance.Roll(3, 0) <= populationRating - 3); // Puede haber más de uno

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
