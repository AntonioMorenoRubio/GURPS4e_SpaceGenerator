using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;
using GeneratorLibrary.Tables;
using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Generators
{
    public class WorldGenerator
    {
        private int SettingTL = 10;
        private readonly DiceRoller _diceRoller = DiceRoller.Instance;
        private readonly IRandomProvider _randomProvider = new RandomProvider();

        public WorldGenerator() { }

        public WorldGenerator(int TL, int seed)
        {
            SettingTL = TL;
            _diceRoller = new(seed);
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
            world.Type = new WorldType(size, subType);

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

                Atmosphere? atm = AtmosphereTables.ApplyMarginalAtmosphere(atmosphere, _randomProvider);
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

            if (world.Type.SubType is WorldSubType.GasGiant)
            {

            }
            else if (world.Type.SubType is WorldSubType.AsteroidBelt)
                population.CarryingCapacity = PopulationTables.CalculateAsteroidCarryingCapacity(
                    world.TechLevel.TL,
                    world.ResourcesHabitability.Affinity);
            else
                population.CarryingCapacity = PopulationTables.CalculateWorldCarryingCapacity(
                    world.TechLevel.TL,
                    world.ResourcesHabitability.Affinity,
                    world.Characteristics?.Diameter);


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

                world.Society = society;
            }

            return world;
        }
    }
}
