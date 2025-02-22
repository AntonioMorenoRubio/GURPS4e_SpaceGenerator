using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;
using GeneratorLibrary.Tests.Utils;

namespace GeneratorLibrary.Tests
{
    public class AtmosphereTablesTests
    {
        public static IEnumerable<object[]> WorldsWithAtmosphericComposition => new List<object[]>
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

        public static IEnumerable<object[]> WorldsWithAtmosphereCharacteristics => new List<object[]>
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
        public void AtmosphereTables_WorldType_AssignZeroMass(WorldSize size, WorldSubType subType)
        {
            //Act
            double mass = AtmosphereTables.GenerateMass(size, subType);

            //Assert
            Assert.Equal(0f, mass);
        }

        [Theory]
        [InlineData(WorldSize.Small, WorldSubType.Ice)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean)]
        [InlineData(WorldSize.Standard, WorldSubType.Ice)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean)]
        [InlineData(WorldSize.Large, WorldSubType.Ice)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden)]
        [InlineData(WorldSize.Large, WorldSubType.Garden)]
        [InlineData(WorldSize.Special, WorldSubType.GasGiant)]
        public void AtmosphereTables_WorldType_GenerateCorrectRandomMass(WorldSize size, WorldSubType subType)
        {
            //Act
            double mass = AtmosphereTables.GenerateMass(size, subType);

            //Assert
            Assert.InRange(mass, 0.25f, 1.85f);
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
        [MemberData(nameof(WorldsWithAtmosphericComposition))]
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
        [MemberData(nameof(WorldsWithAtmosphereCharacteristics))]
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
        [InlineData(2)]
        [InlineData(19)]
        [InlineData(0)]
        [InlineData(-1)]
        public void GenerateMarginalAtmosphere_InvalidRoll_ShouldThrowArgumentOutOfRangeException(int invalidRoll)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => AtmosphereTables.GenerateMarginalAtmosphere(invalidRoll));

            // Verificamos que el mensaje de la excepción contenga el número incorrecto
            Assert.Contains($"Roll:{invalidRoll}", exception.Message);
        }

        [Theory]
        [InlineData(null, true, true, true)]  // Caso 1: Atmosphere es null
        [InlineData(true, null, true, true)]  // Caso 2: Composition es null
        [InlineData(true, true, null, true)]  // Caso 3: Characteristics es null
        [InlineData(true, true, true, null)]  // Caso 4: MarginalAtmosphere es null
        public void ApplyMarginalAtmosphere_InvalidInputs_ShouldReturnNull(
        bool? createAtmosphere, bool? hasComposition, bool? hasCharacteristics, bool? hasMarginal)
        {
            // Arrange
            Atmosphere? atmosphere = createAtmosphere == true ? new Atmosphere() : null;

            if (atmosphere is not null)
            {
                atmosphere.Composition = hasComposition == true ? new List<string>() : null;
                atmosphere.Characteristics = hasCharacteristics == true ? new List<AtmosphereCharacteristic>() : null;
                atmosphere.MarginalAtmosphere = hasMarginal == true ? MarginalAtmosphere.HighCarbonDioxide : null;
            }

            var randomProvider = new TestRandomProvider(0.5); // No importa el valor aquí

            // Act
            var result = AtmosphereTables.ApplyMarginalAtmosphere(atmosphere, randomProvider);

            // Assert
            Assert.Null(result);
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

            TestRandomProvider? testRandomProvider = new TestRandomProvider(probability);

            // Act
            var result = AtmosphereTables.ApplyMarginalAtmosphere(atmosphere, testRandomProvider);

            // Assert
            Assert.Equal(expectedComposition, result?.Composition);
            Assert.Equal(expectedCharacteristics, result?.Characteristics);
        }

    }
}
