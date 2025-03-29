using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;
using GeneratorLibrary.Tests.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class AtmosphereTablesTests
    {
        #region TestData
        public static IEnumerable<object[]> WorldsWithoutAtmosphere => new List<object[]>
        {
        new object [] { WorldSize.Special, WorldSubType.AsteroidBelt },
        new object [] { WorldSize.Tiny, WorldSubType.Ice },
        new object [] { WorldSize.Tiny, WorldSubType.Rock },
        new object [] { WorldSize.Tiny, WorldSubType.Sulfur },
        new object [] { WorldSize.Small, WorldSubType.Hadean },
        new object [] { WorldSize.Small, WorldSubType.Rock },
        new object [] { WorldSize.Standard, WorldSubType.Hadean },
        new object [] { WorldSize.Standard, WorldSubType.Chthonian },
        new object [] { WorldSize.Large, WorldSubType.Chthonian },
        };

        public static IEnumerable<object[]> WorldsWithoutAtmosphereRollTestData()
        {
            foreach (var world in WorldsWithoutAtmosphere)
                foreach (var roll in DiceRollerTests.AllTestDiceRolls())
                    yield return world.Append(roll[0]).ToArray();
        }

        public static IEnumerable<object[]> WorldsWithAtmosphere => new List<object[]>
        {
        new object [] { WorldSize.Small, WorldSubType.Ice },
        new object [] { WorldSize.Standard, WorldSubType.Greenhouse },
        new object [] { WorldSize.Standard, WorldSubType.Ammonia },
        new object [] { WorldSize.Standard, WorldSubType.Ocean },
        new object [] { WorldSize.Standard, WorldSubType.Ice },
        new object [] { WorldSize.Large, WorldSubType.Ammonia },
        new object [] { WorldSize.Large, WorldSubType.Greenhouse },
        new object [] { WorldSize.Large, WorldSubType.Ocean },
        new object [] { WorldSize.Large, WorldSubType.Ice },
        new object [] { WorldSize.Standard, WorldSubType.Garden },
        new object [] { WorldSize.Large, WorldSubType.Garden },
        new object [] { WorldSize.Special, WorldSubType.GasGiant }
        };

        public static IEnumerable<object[]> WorldsWithAtmosphere_ValidRollTestData()
        {
            foreach (var world in WorldsWithAtmosphere)
                foreach (var roll in DiceRollerTests.Valid3dDiceRollValues())
                    yield return world.Append(roll[0]).ToArray();
        }

        public static IEnumerable<object[]> WorldsWithAtmosphere_InvalidRollTestData()
        {
            foreach (var world in WorldsWithAtmosphere)
                foreach (var roll in DiceRollerTests.Invalid3dDiceRollValues())
                    yield return world.Append(roll[0]).ToArray();
        }

        public const double MINIMUM_MASS = 3 / 10.0 - 0.05;
        public const double MAXIMUM_MASS = 18 / 10.0 + 0.05;

        public static IEnumerable<object[]> AtmosphericCompositionForWorlds => new List<object[]>
        {
            new object[] { WorldSize.Small, WorldSubType.Ice, new List<string> { "Nitrogen", "Methane" } },
            new object[] { WorldSize.Standard, WorldSubType.Ammonia, new List<string> { "Nitrogen", "Ammonia","Methane" } },
            new object[] { WorldSize.Standard, WorldSubType.Ice, new List<string> { "Carbon Dioxide", "Nitrogen" } },
            new object[] { WorldSize.Standard, WorldSubType.Ocean, new List<string> { "Carbon Dioxide", "Nitrogen" } },
            new object[] { WorldSize.Standard, WorldSubType.Garden, new List<string> { "Nitrogen", "Oxygen" } },
            new object[] { WorldSize.Standard, WorldSubType.Greenhouse, new List<string> { "Carbon Dioxide", "Nitrogen" } },
            new object[] { WorldSize.Large, WorldSubType.Ammonia, new List<string> { "Helium", "Ammonia","Methane" } },
            new object[] { WorldSize.Large, WorldSubType.Ice, new List<string> { "Helium", "Nitrogen" } },
            new object[] { WorldSize.Large, WorldSubType.Ocean, new List<string> { "Helium", "Nitrogen" } },
            new object[] { WorldSize.Large, WorldSubType.Garden, new List<string> { "Nitrogen", "Oxygen", "Noble gases" } },
            new object[] { WorldSize.Large, WorldSubType.Greenhouse, new List<string> { "Carbon Dioxide", "Nitrogen" } },
            new object[] { WorldSize.Special, WorldSubType.GasGiant, new List<string> { "Hydrogen", "Helium" } }
        };

        public static IEnumerable<object[]> AtmosphereCharacteristicsForWorlds => new List<object[]>
        {
            new object[] { WorldSize.Small, WorldSubType.Ice, 15, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.MildlyToxic } },

            new object[] { WorldSize.Small, WorldSubType.Ice, 16, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.HighlyToxic } },

            new object[] { WorldSize.Standard, WorldSubType.Ammonia, 0, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.LethallyToxic, AtmosphereCharacteristic.Corrosive } },

            new object[] { WorldSize.Standard, WorldSubType.Ice, 12, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating } },

            new object[] { WorldSize.Standard, WorldSubType.Ice, 13, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.MildlyToxic } },

            new object[] { WorldSize.Standard, WorldSubType.Ocean, 12, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating } },

            new object[] { WorldSize.Standard, WorldSubType.Ocean, 13, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.MildlyToxic } },

            new object[] { WorldSize.Standard, WorldSubType.Garden, 11, new List<AtmosphereCharacteristic>() },
            new object[] { WorldSize.Standard, WorldSubType.Garden, 12, new List<AtmosphereCharacteristic>
            { AtmosphereCharacteristic.Marginal } },

            new object[] { WorldSize.Standard, WorldSubType.Greenhouse, 0, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.LethallyToxic, AtmosphereCharacteristic.Corrosive } },

            new object[] { WorldSize.Large, WorldSubType.Ammonia, 0, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.LethallyToxic, AtmosphereCharacteristic.Corrosive } },

            new object[] { WorldSize.Large, WorldSubType.Ice, 0, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.HighlyToxic } },

            new object[] { WorldSize.Large, WorldSubType.Ocean, 0, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating, AtmosphereCharacteristic.HighlyToxic } },

            new object[] { WorldSize.Large, WorldSubType.Garden, 11, new List<AtmosphereCharacteristic>() },
            new object[] { WorldSize.Large, WorldSubType.Garden, 12, new List<AtmosphereCharacteristic>
            { AtmosphereCharacteristic.Marginal } },

            new object[] { WorldSize.Large, WorldSubType.Greenhouse, 0, new List<AtmosphereCharacteristic>
                { AtmosphereCharacteristic.Suffocating } }
        };

        public static IEnumerable<object[]> AtmosphereMarginalTestData => new List<object[]>
        {
            new object[]
            {
                MarginalAtmosphere.ChlorineOrFluorine, 0.85, // 85% → "Chlorine"
                new List<string> { "Chlorine" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.HighlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.ChlorineOrFluorine, 0.90, // 90% → "Chlorine"
                new List<string> { "Chlorine" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.HighlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.ChlorineOrFluorine, 0.90001, // 90+% → "Fluorine"
                new List<string> { "Fluorine" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.HighlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.ChlorineOrFluorine, 0.95, // 95% → "Fluorine"
                new List<string> { "Fluorine" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.HighlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.HighCarbonDioxide, 0,
                new List<string> { "Carbon Dioxide" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.MildlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.NitrogenCompounds, 0,
                new List<string> { "Nitrogen Oxide" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.MildlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.SulphurCompounds, 0,
                new List<string> { "Hydrogen Sulfide", "Sulfur Dioxide", "Sulfur Trioxide" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.MildlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.OrganicToxins, 0,
                new List<string> { "Spores" },
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.MildlyToxic }
            },
            new object[]
            {
                MarginalAtmosphere.Pollutants, 0,
                new List<string>(),
                new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.MildlyToxic }
            },
            new object[]
            {
                (MarginalAtmosphere)999, 0, // Caso de MarginalAtmosphere desconocido
                new List<string>(),
                new List<AtmosphereCharacteristic>()
            }
        };
        #endregion

        [Theory]
        [MemberData(nameof(WorldsWithoutAtmosphereRollTestData))]
        public void AtmosphereTables_WorldType_AssignZeroMass(WorldSize size, WorldSubType subType, int roll)
        {
            //Act
            double mass = AtmosphereTables.GenerateMass(size, subType, roll);

            //Assert
            Assert.Equal(0f, mass);
        }

        [Theory]
        [MemberData(nameof(WorldsWithAtmosphere_ValidRollTestData))]
        public void AtmosphereTables_WorldType_ValidRoll_GenerateCorrectRandomMass(WorldSize size, WorldSubType subType, int roll)
        {
            //Act
            double mass = AtmosphereTables.GenerateMass(size, subType, roll);

            //Assert
            Assert.InRange(mass, MINIMUM_MASS, MAXIMUM_MASS);
        }

        [Theory]
        [MemberData(nameof(WorldsWithAtmosphere_InvalidRollTestData))]
        public void AtmosphereTables_WorldType_InvalidRoll_ThrowsArgumentOutOfRangeException(WorldSize size, WorldSubType subType, int roll)
        {
            //Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => AtmosphereTables.GenerateMass(size, subType, roll));
        }

        [Theory]
        [InlineData(10000)]
        [InlineData(1000000)]
        public void Variation_ShouldAlwaysBeBetweenMinMaxValues(int iterations)
        {
            //Arrange
            double min = -0.05;
            double max = 0.05;

            //Act
            for (int i = 0; i < iterations; i++)
            {
                double actual = Random.Shared.NextDouble() * 0.1 - 0.05;

                //Assert
                Assert.InRange(actual, min, max);
            }
        }

        [Theory]
        [InlineData(WorldSize.Special, WorldSubType.AsteroidBelt)]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean)]
        [InlineData(WorldSize.Small, WorldSubType.Rock)]
        [InlineData(WorldSize.Standard, WorldSubType.Hadean)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian)]
        public void GetComposition_NoAtmosphereWorlds_ShouldReturnEmpty(WorldSize size, WorldSubType subType)
        {
            //Act
            List<string> list = AtmosphereTables.GetComposition(size, subType);

            //Assert
            Assert.Empty(list);
        }

        [Theory]
        [MemberData(nameof(AtmosphericCompositionForWorlds))]
        public void GetComposition_WorldsWithAtmosphere_ShouldReturnComposition(WorldSize size, WorldSubType subType, List<string> expected)
        {
            //Act
            List<string> actual = AtmosphereTables.GetComposition(size, subType);

            //Assert
            foreach (var item in expected)
                Assert.Contains(item, actual);

            foreach (var item in actual)
                Assert.Contains(item, expected);
        }

        [Theory]
        [MemberData(nameof(AtmosphereCharacteristicsForWorlds))]
        public void GenerateCharacteristics_WorldsWithAtmosphere_ShouldReturnPossibleCharacteristics(WorldSize size, WorldSubType subType, int roll, List<AtmosphereCharacteristic> expected)
        {
            //Act
            List<AtmosphereCharacteristic> actual = AtmosphereTables.GenerateCharacteristics(size, subType, roll);

            //Assert
            foreach (var item in expected)
                Assert.Contains(item, actual);

            foreach (var item in actual)
                Assert.Contains(item, expected);
        }

        [Theory]
        [InlineData(3, MarginalAtmosphere.ChlorineOrFluorine)]
        [InlineData(4, MarginalAtmosphere.ChlorineOrFluorine)]
        [InlineData(5, MarginalAtmosphere.SulphurCompounds)]
        [InlineData(6, MarginalAtmosphere.SulphurCompounds)]
        [InlineData(7, MarginalAtmosphere.NitrogenCompounds)]
        [InlineData(8, MarginalAtmosphere.OrganicToxins)]
        [InlineData(9, MarginalAtmosphere.OrganicToxins)]
        [InlineData(10, MarginalAtmosphere.LowOxygen)]
        [InlineData(11, MarginalAtmosphere.LowOxygen)]
        [InlineData(12, MarginalAtmosphere.Pollutants)]
        [InlineData(13, MarginalAtmosphere.Pollutants)]
        [InlineData(14, MarginalAtmosphere.HighCarbonDioxide)]
        [InlineData(15, MarginalAtmosphere.HighOxygen)]
        [InlineData(16, MarginalAtmosphere.HighOxygen)]
        [InlineData(17, MarginalAtmosphere.InertGases)]
        [InlineData(18, MarginalAtmosphere.InertGases)]
        public void GenerateMarginalAtmosphere_ValidRoll_ShouldReturnCorrectAtmosphere(int roll, MarginalAtmosphere expected)
        {
            // Act
            var result = AtmosphereTables.GenerateMarginalAtmosphere(roll);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(DiceRollerTests.Invalid3dDiceRollValues), MemberType = typeof(DiceRollerTests))]
        public void GenerateMarginalAtmosphere_InvalidRoll_ShouldThrowArgumentOutOfRangeException(int invalidRoll)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => AtmosphereTables.GenerateMarginalAtmosphere(invalidRoll));

            // Verificamos que el mensaje de la excepción contenga el número incorrecto
            Assert.Contains($"Roll:{invalidRoll}", exception.Message);
        }

        [Theory]
        [InlineData(false, MarginalAtmosphere.None)]  // Caso 1: Atmosphere es null
        [InlineData(true, MarginalAtmosphere.None)]  // Caso 2: Atmosphere tiene MarginalAtmosphere.None
        public void ApplyMarginalAtmosphere_InvalidInputs_ShouldReturnSameAtmosphere(bool createAtmosphere, MarginalAtmosphere marginal)
        {
            // Arrange
            Atmosphere? atmosphere = createAtmosphere ? new Atmosphere() : null;

            if (atmosphere is not null)
                atmosphere.MarginalAtmosphere = marginal;

            // Act
            var actual = AtmosphereTables.ApplyMarginalAtmosphere(atmosphere, 0.5);

            // Assert
            Assert.Equal(atmosphere, actual);
        }

        [Theory]
        [MemberData(nameof(AtmosphereMarginalTestData))]
        public void ApplyMarginalAtmosphere_ShouldApplyCorrectChanges(
            MarginalAtmosphere marginalType, double probability,
            List<string> expectedComposition, List<AtmosphereCharacteristic> expectedCharacteristics)
        {
            // Arrange
            Atmosphere atmosphere = new()
            {
                Composition = new List<string>(),
                Characteristics = new List<AtmosphereCharacteristic>(),
                MarginalAtmosphere = marginalType
            };

            // Act
            var result = AtmosphereTables.ApplyMarginalAtmosphere(atmosphere, probability);

            // Assert
            Assert.Equal(expectedComposition, result?.Composition);
            Assert.Equal(expectedCharacteristics, result?.Characteristics);
        }

        [Theory]
        [InlineData(WorldSize.Special, WorldSubType.AsteroidBelt, 1.0, 1.0, 0.0)]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice, 1.0, 1.0, 0.0)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock, 2.0, 1.5, 0.0)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur, 2.0, 1.5, 0.0)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean, 2.0, 1.5, 0.0)]
        [InlineData(WorldSize.Standard, WorldSubType.Hadean, 2.0, 1.5, 0.0)]
        [InlineData(WorldSize.Small, WorldSubType.Rock, 3.0, 2.0, 0.01)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian, 3.0, 2.0, 0.01)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian, 3.0, 2.0, 0.01)]
        [InlineData(WorldSize.Small, WorldSubType.Ice, 2.0, 1.0, 20.0)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia, 2.0, 1.0, 2.0)]
        [InlineData(WorldSize.Standard, WorldSubType.Ice, 2.0, 2.0, 4.0)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 3.0, 1.0, 3.0)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 2.0, 4.0, 8.0)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse, 2.0, 1.0, 200.0)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia, 2.0, 1.0, 10.0)]
        [InlineData(WorldSize.Large, WorldSubType.Ice, 2.0, 2.0, 20.0)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 3.0, 1.0, 15.0)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 2.0, 4.0, 40.0)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse, 2.0, 2.0, 2000.0)]
        public void GenerateAtmosphericPressure_ShouldReturnExpectedPressure(
            WorldSize size, WorldSubType subType, double atmosphereMass, double surfaceGravity, double expectedPressure)
        {
            // Act
            double result = AtmosphereTables.GenerateAtmosphericPressure(size, subType, atmosphereMass, surfaceGravity);

            // Assert
            Assert.Equal(expectedPressure, result, precision: 2);
        }

        [Fact]
        public void GenerateAtmosphericPressure_GasGiant_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => AtmosphereTables.GenerateAtmosphericPressure(WorldSize.Special, WorldSubType.GasGiant, 1.0, 1.0));
        }

        [Theory]
        [InlineData(0.005, PressureCategory.Trace)]
        [InlineData(0.009, PressureCategory.Trace)]
        [InlineData(0.01, PressureCategory.VeryThin)]
        [InlineData(0.509, PressureCategory.VeryThin)]
        [InlineData(0.51, PressureCategory.Thin)]
        [InlineData(0.809, PressureCategory.Thin)]
        [InlineData(0.81, PressureCategory.Standard)]
        [InlineData(1.0, PressureCategory.Standard)]
        [InlineData(1.209, PressureCategory.Standard)]
        [InlineData(1.21, PressureCategory.Dense)]
        [InlineData(1.509, PressureCategory.Dense)]
        [InlineData(1.51, PressureCategory.VeryDense)]
        [InlineData(5.0, PressureCategory.VeryDense)]
        [InlineData(10.0, PressureCategory.VeryDense)]
        [InlineData(10.001, PressureCategory.Superdense)]
        [InlineData(20.0, PressureCategory.Superdense)]
        [InlineData(50.0, PressureCategory.Superdense)]
        [InlineData(100.0, PressureCategory.Superdense)]
        public void GetPressureCategory_ShouldReturnCorrectCategory(double pressure, PressureCategory expectedCategory)
        {
            // Act
            var result = AtmosphereTables.GetPressureCategory(pressure);

            // Assert
            Assert.Equal(expectedCategory, result);
        }

        [Theory]
        [InlineData(double.NaN)]
        public void GetPressureCategory_ShouldThrowException(double pressure)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => AtmosphereTables.GetPressureCategory(pressure));
        }
    }
}
