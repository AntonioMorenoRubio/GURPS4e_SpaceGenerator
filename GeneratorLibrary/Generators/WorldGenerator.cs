using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;
using GeneratorLibrary.Tables;
using GeneratorLibrary.Utils;

namespace GeneratorLibrary.Generators
{
    public class WorldGenerator
    {
        private readonly DiceRoller _diceRoller = DiceRoller.Instance;
        private readonly IRandomProvider _randomProvider = new RandomProvider();

        public WorldGenerator() { }

        public WorldGenerator(int seed)
        {
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
            Atmosphere atmosphere = new();
            atmosphere.Mass = AtmosphereTables.GenerateMass(world.Type.Size, world.Type.SubType);
            atmosphere.Composition = AtmosphereTables.GetComposition(world.Type.Size, world.Type.SubType);
            roll1 = _diceRoller.Roll();
            atmosphere.Characteristics = AtmosphereTables.GenerateCharacteristics(world.Type.Size, world.Type.SubType, roll1);

            if (atmosphere.Characteristics.Contains(AtmosphereCharacteristic.Marginal))
            {
                atmosphere.Characteristics.Remove(AtmosphereCharacteristic.Marginal);
                roll1 = _diceRoller.Roll();
                atmosphere.MarginalAtmosphere = AtmosphereTables.GenerateMarginalAtmosphere(roll1);

                roll1 = _diceRoller.Roll();
                Atmosphere? atm = AtmosphereTables.ApplyMarginalAtmosphere(atmosphere, _randomProvider);
                if (atm is not null)
                    atmosphere = atm;
            }

            world.Atmosphere = atmosphere;

            //STEP 4: Hydrographic Coverage
            HydrographicCoverage hydrographicCoverage = new HydrographicCoverage();

            hydrographicCoverage.Coverage = HydrographicCoverageTables.GenerateHydrographicCoverage(world.Type.Size, world.Type.SubType);
            hydrographicCoverage.Composition = HydrographicCoverageTables.GetHydrographicComposition(world.Type.Size, world.Type.SubType);

            world.HydrographicCoverage = hydrographicCoverage;

            return world;
        }
    }
}
