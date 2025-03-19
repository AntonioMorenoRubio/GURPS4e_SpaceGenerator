using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class ResourceHabitabilityTablesTests
    {
        public static IEnumerable<object[]> WorldsTestData => new List<object[]>
        {
            // Mundo sin atmósfera (Vacío)
            new object[]
            {
                new World
                {
                    Atmosphere = new Atmosphere { PressureCategory = PressureCategory.Trace },
                    HydrographicCoverage = new HydrographicCoverage { Coverage = 0.0, Composition = new() },
                    Climate = new Climate { ClimateType = ClimateType.Frozen }
                },
                new List<int> { 0, 0 } // Sin atmósfera o Trace (0), Cobertura Hidrográfica 0 (0)
            },

            // Mundo con atmósfera respirable pero como la anterior
            new object[]
            {
                new World
                {
                    Atmosphere = new Atmosphere
                    {
                        PressureCategory = PressureCategory.Trace,
                        Composition = new List<string> { "Nitrogen", "Oxygen" },
                        Characteristics = new List<AtmosphereCharacteristic> ()
                    },
                    HydrographicCoverage = new HydrographicCoverage { Coverage = 0.0, Composition = new List<string>() },
                    Climate = new Climate { ClimateType = ClimateType.Frozen }
                },
                new List<int> { 0, 0 } // Atmósfera Trace (No respirable, B429) aunque tenga oxígeno (0), océanos 0% (0)
            },
        
            // Mundo con atmósfera tóxica y corrosiva
            new object[]
            {
                new World
                {
                    Atmosphere = new Atmosphere
                    {
                        PressureCategory = PressureCategory.Standard,
                        Composition = new List<string> { "Nitrogen", "Carbon Dioxide" },
                        Characteristics = new List<AtmosphereCharacteristic> {
                            AtmosphereCharacteristic.Suffocating,
                            AtmosphereCharacteristic.LethallyToxic,
                            AtmosphereCharacteristic.Corrosive
                        }
                    },
                    HydrographicCoverage = new HydrographicCoverage { Coverage = 50.0, Composition = new List<string> { "Liquid Water" } },
                    Climate = new Climate { ClimateType = ClimateType.Normal }
                },
                new List<int> { -2, 1 } // Atmósfera letal (-2), océanos 1-59% (+1), clima no afecta (-)
            },

            // Mundo con atmósfera tóxica y corrosiva pero respirable
            new object[]
            {
                new World
                {
                    Atmosphere = new Atmosphere
                    {
                        PressureCategory = PressureCategory.Standard,
                        Composition = new List<string> { "Nitrogen", "Oxygen" },
                        Characteristics = new List<AtmosphereCharacteristic> {
                            AtmosphereCharacteristic.Suffocating,
                            AtmosphereCharacteristic.LethallyToxic,
                            AtmosphereCharacteristic.Corrosive
                        }
                    },
                    HydrographicCoverage = new HydrographicCoverage { Coverage = 50.0, Composition = new List<string> { "Liquid Water" } },
                    Climate = new Climate { ClimateType = ClimateType.Normal }
                },
                new List<int> { 3, 1, 1, 2 } // Atmósfera respirable estándar (+3), atmósfera no marginal (+1), océanos 1-59% (+1), clima templado (+2)
            },
        
            // Mundo con atmósfera respirable, océanos y clima templado
            new object[]
            {
                new World
                {
                    Atmosphere = new Atmosphere
                    {
                        PressureCategory = PressureCategory.Standard,
                        Composition = new List<string> { "Nitrogen", "Oxygen" },
                        Characteristics = new List<AtmosphereCharacteristic> ()
                    },
                    HydrographicCoverage = new HydrographicCoverage { Coverage = 70.0, Composition = new List<string> { "Liquid Water" } },
                    Climate = new Climate { ClimateType = ClimateType.Warm }
                },
                new List<int> { 3, 1, 2, 2 } // Atmósfera estándar (+3), Atmósfera no marginal (+1), océanos 60-90% (+2), clima templado cálido (+2)
            }
        };


        [Theory]
        [InlineData(3, -5)]
        [InlineData(4, -4)]
        [InlineData(5, -3)]
        [InlineData(6, -2)]
        [InlineData(7, -2)]
        [InlineData(8, -1)]
        [InlineData(9, -1)]
        [InlineData(10, 0)]
        [InlineData(11, 0)]
        [InlineData(12, 1)]
        [InlineData(13, 1)]
        [InlineData(14, 2)]
        [InlineData(15, 2)]
        [InlineData(16, 3)]
        [InlineData(17, 4)]
        [InlineData(18, 5)]
        public void ResourceValueForAsteroidBelts_ReturnsCorrectModifier(int roll, int expected)
        {
            //Act
            int actual = ResourceHabitabilityTables.ResourceValueForAsteroidBelts(roll);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-100, -3)]
        [InlineData(0, -3)]
        [InlineData(2, -3)]
        [InlineData(3, -2)]
        [InlineData(4, -2)]
        [InlineData(5, -1)]
        [InlineData(6, -1)]
        [InlineData(7, -1)]
        [InlineData(8, 0)]
        [InlineData(9, 0)]
        [InlineData(10, 0)]
        [InlineData(11, 0)]
        [InlineData(12, 0)]
        [InlineData(13, 0)]
        [InlineData(14, 1)]
        [InlineData(15, 1)]
        [InlineData(16, 1)]
        [InlineData(17, 2)]
        [InlineData(18, 2)]
        [InlineData(19, 3)]
        [InlineData(20, 3)]
        [InlineData(100, 3)]
        public void ResourceValueForOtherWorlds_ReturnsCorrectModifier(int roll, int expected)
        {
            //Act
            int actual = ResourceHabitabilityTables.ResourceValueForOtherWorlds(roll);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-5, ResourceOverallValue.Worthless)]
        [InlineData(-4, ResourceOverallValue.VeryScant)]
        [InlineData(-3, ResourceOverallValue.Scant)]
        [InlineData(-2, ResourceOverallValue.VeryPoor)]
        [InlineData(-1, ResourceOverallValue.Poor)]
        [InlineData(0, ResourceOverallValue.Average)]
        [InlineData(1, ResourceOverallValue.Abundant)]
        [InlineData(2, ResourceOverallValue.VeryAbundant)]
        [InlineData(3, ResourceOverallValue.Rich)]
        [InlineData(4, ResourceOverallValue.VeryRich)]
        [InlineData(5, ResourceOverallValue.Motherlode)]
        public void GetResourceOverallValue_ReturnsCorrectValue(int resourceValueModifier, ResourceOverallValue expected)
        {
            //Act
            ResourceOverallValue actual = ResourceHabitabilityTables.GetResourceOverallValue(resourceValueModifier);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(-10)]
        [InlineData(-6)]
        [InlineData(6)]
        [InlineData(10)]
        [InlineData(100)]
        public void GetResourceOverallValue_InvalidModifier_ShouldThrowArgumentOutOfRangeException(int resourceValueModifier)
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ResourceHabitabilityTables.GetResourceOverallValue(resourceValueModifier));
            Assert.Contains("No rule for resourceValueModifier", ex.Message);
        }

        [Theory]
        [MemberData(nameof(WorldsTestData))]
        public void GetHabitabilityModifiers_ShouldReturnExpectedModifiers(World world, List<int> expectedModifiers)
        {
            // Act
            List<int> actualModifiers = ResourceHabitabilityTables.GetHabitabilityModifiers(world);

            // Assert
            Assert.Equal(expectedModifiers.OrderBy(x => x), actualModifiers.OrderBy(x => x));
        }
    }
}
