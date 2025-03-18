using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;
using GeneratorLibrary.Tables;

namespace GeneratorLibrary.Generators
{
    public class WorldGenerator
    {
        private readonly int SettingTL = 10;
        private readonly DiceRoller _diceRoller = DiceRoller.Instance;
        private readonly Random _random = Random.Shared;

        public WorldGenerator() { }

        public WorldGenerator(int TL, int seed)
        {
            SettingTL = TL;
            _diceRoller = new(seed);
            _random = new(seed);
        }

        public World GenerateWorld()
        {
            World world = new();

            int roll1, roll2;

            //STEP 2: Generate World Type
            roll1 = _diceRoller.Roll();
            roll2 = _diceRoller.Roll();
            string overallType = WorldTypeTables.GetOverallType(roll1);
            (WorldSize size, WorldSubType subType) = WorldTypeTables.GenerateWorldType(overallType, roll2);
            world.Type = new WorldType
            {
                Size = size,
                SubType = subType
            };

            //STEP 3: Generate Atmosphere
            Atmosphere atmosphere = new()
            {
                Mass = AtmosphereTables.GenerateMass(world.Type.Size, world.Type.SubType),
                Composition = AtmosphereTables.GetComposition(world.Type.Size, world.Type.SubType)
            };

            roll1 = _diceRoller.Roll();
            atmosphere.Characteristics = AtmosphereTables.GenerateCharacteristics(world.Type.Size, world.Type.SubType, roll1);

            if (atmosphere.Characteristics.Remove(AtmosphereCharacteristic.Marginal))
            {
                roll1 = _diceRoller.Roll();
                atmosphere.MarginalAtmosphere = AtmosphereTables.GenerateMarginalAtmosphere(roll1);

                Atmosphere? atm = AtmosphereTables.ApplyMarginalAtmosphere(atmosphere, randomValue: _random.NextDouble());
                if (atm is not null)
                    atmosphere = atm;
            }

            world.Atmosphere = atmosphere;

            //STEP 4: Hydrographic Coverage
            HydrographicCoverage hydrographicCoverage = new()
            {
                Coverage = HydrographicCoverageTables.GenerateHydrographicCoverage(world.Type.Size, world.Type.SubType),
                Composition = HydrographicCoverageTables.GetHydrographicComposition(world.Type.Size, world.Type.SubType)
            };

            world.HydrographicCoverage = hydrographicCoverage;

            //STEP 5: Climate
            Climate climate = new()
            {
                BlackBodyCorrection = Math.Round(ClimateTables.GenerateBlackbodyCorrection(world.Type.Size, world.Type.SubType, world.Atmosphere.Mass, world.HydrographicCoverage.Coverage), 4),
                Kelvin = Math.Round(ClimateTables.GenerateAverageSurfaceTemperatureInKelvinsByWorldType(world.Type.Size, world.Type.SubType), 0)
            };
            climate.ClimateType = ClimateTables.GetClimateTypeBasedOnKelvinTemperature(climate.Kelvin);
            climate.BlackBodyTemperature = Math.Round(climate.Kelvin / climate.BlackBodyCorrection, 2);

            world.Climate = climate;

            //STEP 6: World Size (Characteristics)
            if (world.Type.Size is not WorldSize.Special)
            {
                Characteristics characteristics = new()
                {
                    Density = Math.Round(CharacteristicsTables.GenerateWorldDensity(world.Type.Size, world.Type.SubType), 2)
                };
                characteristics.Diameter =
                    Math.Round(CharacteristicsTables.GenerateWorldDiameter(world.Type.Size, world.Climate.BlackBodyTemperature, characteristics.Density), 2);
                characteristics.SurfaceGravity = Math.Round(CharacteristicsTables.GenerateWorldSurfaceGravity(characteristics.Diameter, characteristics.Density), 2);
                characteristics.Mass = Math.Round(characteristics.Density * Math.Pow(characteristics.Diameter, 3), 2);

                world.Characteristics = characteristics;
            }

            //Atmospheric Pressure that could not be determined without Surface Gravity
            if (world.Characteristics is null)
                world.Atmosphere.Pressure = Math.Round(
                    AtmosphereTables.GenerateAtmosphericPressure(world.Type.Size, world.Type.SubType, world.Atmosphere.Mass, 0),
                    2);
            else
                world.Atmosphere.Pressure = Math.Round(
                    AtmosphereTables.GenerateAtmosphericPressure(world.Type.Size, world.Type.SubType, world.Atmosphere.Mass, world.Characteristics.SurfaceGravity),
                    2);

            world.Atmosphere.PressureCategory = AtmosphereTables.GetPressureCategory(world.Atmosphere.Pressure);

            //STEP 7: Resources and Habitability
            ResourcesHabitability resourcesHabitability = new();
            roll1 = DiceRoller.Instance.Roll();

            if (world.Type.SubType is WorldSubType.AsteroidBelt)
                resourcesHabitability.ResourceValueModifier = ResourceHabitabilityTables.ResourceValueForAsteroidBelts(roll1);
            else
                resourcesHabitability.ResourceValueModifier = ResourceHabitabilityTables.ResourceValueForOtherWorlds(roll1);

            resourcesHabitability.ResourceOverall = ResourceHabitabilityTables.GetResourceOverallValue(resourcesHabitability.ResourceValueModifier);
            resourcesHabitability.HabitabilityModifiers = ResourceHabitabilityTables.GetHabitabilityModifiers(world);

            world.ResourcesHabitability = resourcesHabitability;

            //STEP 8: Settlement Type
            //There are no rules in the manual (S89-90), so we have to 'make up' some rules.
            SettlementData settlementData = new();
            roll1 = DiceRoller.Instance.Roll(1);
            roll2 = DiceRoller.Instance.Roll(1);
            settlementData.IsInClaimedSpace = roll1 % 2 == 0;
            settlementData.Type = (roll2 == 3 || roll2 == 4) ? SettlementType.Homeworld : SettlementDataTables.DetermineSettlementType
            (
                world.ResourcesHabitability.Affinity,
                settlementData.IsInClaimedSpace,
                false
            );

            world.SettlementData = settlementData;

            //STEP 9: Tech Level
            TechLevel techLevel = new();
            roll1 = DiceRoller.Instance.Roll();
            techLevel.Status = TechLevelTables.DetermineTechStatus(world.SettlementData, world.ResourcesHabitability.Habitability, roll1);
            if (techLevel.Status is TechStatus.Primitive)
            {
                roll2 = DiceRoller.Instance.Roll();
                techLevel.TL = TechLevelTables.DetermineTechLevel(techLevel.Status, world.ResourcesHabitability.Habitability, SettingTL, roll2);
            }
            else
                techLevel.TL = TechLevelTables.DetermineTechLevel(techLevel.Status, world.ResourcesHabitability.Habitability, SettingTL);

            world.TechLevel = techLevel;

            //STEP 10: Population
            Population population = new();

            if (world.Type.SubType is not WorldSubType.GasGiant)
            {
                if (world.Type.SubType is WorldSubType.AsteroidBelt)
                    population.CarryingCapacity = PopulationTables.CalculateAsteroidCarryingCapacity(
                        world.TechLevel.TL,
                        world.ResourcesHabitability.Affinity);
                else
                    population.CarryingCapacity = PopulationTables.CalculateWorldCarryingCapacity(
                        world.TechLevel.TL,
                        world.ResourcesHabitability.Affinity,
                        world.Characteristics?.Diameter);
            }

            switch (world.SettlementData.Type)
            {
                case SettlementType.Homeworld:
                    roll1 = DiceRoller.Instance.Roll(2);
                    population.CurrentPopulation = PopulationTables.GenerateHomeworldPopulation(population.CarryingCapacity, world.TechLevel.TL, roll1);
                    break;
                case SettlementType.Colony:
                    roll1 = DiceRoller.Instance.Roll(3);
                    population.CurrentPopulation = PopulationTables.GenerateColonyPopulation(world.TechLevel.TL, roll1);
                    break;
                case SettlementType.Outpost:
                    roll1 = DiceRoller.Instance.Roll(3);
                    population.CurrentPopulation = PopulationTables.GenerateOutpostPopulation(roll1);
                    break;
                default:
                    break;
            }

            population.PopulationRating = PopulationTables.CalculatePopulationRating(population.CurrentPopulation);

            world.Population = population;

            //STEP 11: Society Type
            if (world.SettlementData.Type is not SettlementType.Uninhabited)
            {
                if (world.TechLevel.TL <= 7)
                    roll1 = DiceRoller.Instance.Roll(1);
                else
                    roll1 = DiceRoller.Instance.Roll(2);
                Society society = new()
                {
                    Unity = SocietyTypeTables.GenerateWorldUnity(roll1, world.Population.PopulationRating)
                };

                //We will choose one Interstellar Type at Random
                int random = Random.Shared.Next(3);

                switch (random)
                {
                    case 0:
                        society.InterstellarSocietyType = InterstellarSocietyType.AnarchyOrAlliance;
                        break;
                    case 1:
                        society.InterstellarSocietyType = InterstellarSocietyType.Federation;
                        break;
                    case 2:
                        society.InterstellarSocietyType = InterstellarSocietyType.CorporateState;
                        break;
                    case 3:
                        society.InterstellarSocietyType = InterstellarSocietyType.Empire;
                        break;
                }

                roll1 = DiceRoller.Instance.Roll();
                society.SocietyType = SocietyTypeTables.DetermineSocietyType(society.InterstellarSocietyType, world.TechLevel.TL, roll1);

                if (society.Unity == WorldUnity.WorldGovernment_Special)
                {
                    roll1 = DiceRoller.Instance.Roll(3);
                    roll2 = DiceRoller.Instance.Roll(1);
                    int hasTwoSpecialSocieties = DiceRoller.Instance.Roll(1);

                    List<SpecialSociety> specialSocieties = new();

                    while (world.TechLevel.TL <= 7 && roll1 == 18)
                    {
                        roll1 = DiceRoller.Instance.Roll();
                    }

                    bool MatriarchyOverPatriarchy = roll2 % 2 == 0;
                    specialSocieties.Add(SocietyTypeTables.GenerateSpecialSociety(roll1, MatriarchyOverPatriarchy));

                    //Check for second Special Society Type
                    if ((specialSocieties.Contains(SpecialSociety.Subjugated) ||
                    specialSocieties.Contains(SpecialSociety.Socialist) ||
                    specialSocieties.Contains(SpecialSociety.Oligarchy) ||
                    specialSocieties.Contains(SpecialSociety.Meritocracy)) &&
                    hasTwoSpecialSocieties >= 1 && hasTwoSpecialSocieties <= 3)
                    {
                        roll1 = DiceRoller.Instance.Roll();
                        while (world.TechLevel.TL <= 7 && roll1 == 18)
                        {
                            roll1 = DiceRoller.Instance.Roll();
                        }

                        specialSocieties.Add(SocietyTypeTables.GenerateSpecialSociety(roll1, MatriarchyOverPatriarchy));
                    }
                    society.SpecialSocieties = specialSocieties;
                }

                world.Society = society;

                //STEP 12 Control Rating
                if (world.Society is not null)
                {
                    ControlRating controlRating = new();

                    controlRating.CRList.Add(ControlRatingTables.GetControlRatingRange(world.Society.SocietyType));
                    if (world.Society.SpecialSocieties.Count > 0)
                        foreach (var specialSociety in world.Society.SpecialSocieties)
                            controlRating.CRList.Add(ControlRatingTables.GetControlRatingRange(specialSociety));

                    controlRating.MinMaxCR = (
                        controlRating.CRList.Max(x => x.Item1),
                        controlRating.CRList.Max(x => x.Item2)
                        );

                    roll1 = DiceRoller.Instance.Roll(2);
                    controlRating.CR = ControlRatingTables.GenerateControlRatingInRange(controlRating.MinMaxCR.Min, controlRating.MinMaxCR.Max, roll1);

                    world.ControlRating = controlRating;


                    //STEP 13: Economics
                    Economics economics = new Economics
                    {
                        BasePerCapitaIncome = EconomicsTables.GetBasePerCapitaIncome(world.TechLevel.TL),
                        IncomeModifiers = EconomicsTables.GetIncomeModifiers(world.ResourcesHabitability.Affinity, world.Population.PopulationRating)
                    };

                    economics.FinalPerCapitaIncome = EconomicsTables.GetFinalPerCapitaIncome(
                        economics.BasePerCapitaIncome,
                        economics.IncomeModifiers,
                        world.Population.CarryingCapacity,
                        world.Population.CurrentPopulation);

                    economics.TypicalWealthLevel = EconomicsTables.GetTypicalWealthLevel(economics.FinalPerCapitaIncome, economics.BasePerCapitaIncome);
                    economics.EconomicVolume = EconomicsTables.CalculateEconomicVolume(economics.FinalPerCapitaIncome, world.Population.CurrentPopulation);

                    world.Economics = economics;

                    //STEP 14: Bases and Installations
                    Installations installations = new();
                    roll1 = DiceRoller.Instance.Roll();
                    roll2 = DiceRoller.Instance.Roll();
                    int[] rolls = [roll1, roll2, DiceRoller.Instance.Roll(), DiceRoller.Instance.Roll(), DiceRoller.Instance.Roll()];
                    installations.Spaceports = InstallationsTables.DetermineSpaceportClasses(world.Population.PopulationRating, rolls);
                    if (world.Population.PopulationRating > 0)
                        installations.Facilities = InstallationsTables.GenerateFacilities(world.Population.PopulationRating, world.ControlRating.CR, world.TechLevel.TL, installations.Spaceports);

                    world.Installations = installations;
                }
            }

            return world;
        }
    }
}
