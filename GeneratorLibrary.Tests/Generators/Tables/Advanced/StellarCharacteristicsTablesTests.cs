using GeneratorLibrary.Generators.Tables.Advanced;
using GeneratorLibrary.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Generators.Tables.Advanced
{
    public class StellarCharacteristicsTablesTests
    {
        public const double WhiteDwarfLuminosity = 0.001;

        public static readonly IEnumerable<object[]> BasicStellarTestData = new List<object[]>
        {
            new object[] { 0.10, new StellarEvolutionData { Type = "M7", Temperature = 3100, LuminosityMin = 0.0012, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.15, new StellarEvolutionData { Type = "M6", Temperature = 3200, LuminosityMin = 0.0036, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.20, new StellarEvolutionData { Type = "M5", Temperature = 3200, LuminosityMin = 0.0079, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.25, new StellarEvolutionData { Type = "M4", Temperature = 3300, LuminosityMin = 0.015, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.30, new StellarEvolutionData { Type = "M3", Temperature = 3300, LuminosityMin = 0.024, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.35, new StellarEvolutionData { Type = "M3", Temperature = 3400, LuminosityMin = 0.037, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.40, new StellarEvolutionData { Type = "M2", Temperature = 3500, LuminosityMin = 0.054, LuminosityMax = null, MainSequenceSpan = null, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.45, new StellarEvolutionData { Type = "M1", Temperature = 3600, LuminosityMin = 0.07, LuminosityMax = 0.08, MainSequenceSpan = 70, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.50, new StellarEvolutionData { Type = "M0", Temperature = 3800, LuminosityMin = 0.09, LuminosityMax = 0.11, MainSequenceSpan = 59, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.55, new StellarEvolutionData { Type = "K8", Temperature = 4000, LuminosityMin = 0.11, LuminosityMax = 0.15, MainSequenceSpan = 50, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.60, new StellarEvolutionData { Type = "K6", Temperature = 4200, LuminosityMin = 0.13, LuminosityMax = 0.20, MainSequenceSpan = 42, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.65, new StellarEvolutionData { Type = "K5", Temperature = 4400, LuminosityMin = 0.15, LuminosityMax = 0.25, MainSequenceSpan = 37, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.70, new StellarEvolutionData { Type = "K4", Temperature = 4600, LuminosityMin = 0.19, LuminosityMax = 0.35, MainSequenceSpan = 30, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.75, new StellarEvolutionData { Type = "K2", Temperature = 4900, LuminosityMin = 0.23, LuminosityMax = 0.48, MainSequenceSpan = 24, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.80, new StellarEvolutionData { Type = "K0", Temperature = 5200, LuminosityMin = 0.28, LuminosityMax = 0.65, MainSequenceSpan = 20, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.85, new StellarEvolutionData { Type = "G8", Temperature = 5400, LuminosityMin = 0.36, LuminosityMax = 0.84, MainSequenceSpan = 17, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.90, new StellarEvolutionData { Type = "G6", Temperature = 5500, LuminosityMin = 0.45, LuminosityMax = 1.0, MainSequenceSpan = 14, SubGiantSpan = null, GiantSpan = null } },
            new object[] { 0.95, new StellarEvolutionData { Type = "G4", Temperature = 5700, LuminosityMin = 0.56, LuminosityMax = 1.3, MainSequenceSpan = 12, SubGiantSpan = 1.8, GiantSpan = 1.1 } },
            new object[] { 1.00, new StellarEvolutionData { Type = "G2", Temperature = 5800, LuminosityMin = 0.68, LuminosityMax = 1.6, MainSequenceSpan = 10, SubGiantSpan = 1.6, GiantSpan = 1.0 } },
            new object[] { 1.05, new StellarEvolutionData { Type = "G1", Temperature = 5900, LuminosityMin = 0.87, LuminosityMax = 1.9, MainSequenceSpan = 8.8, SubGiantSpan = 1.4, GiantSpan = 0.8 } },
            new object[] { 1.10, new StellarEvolutionData { Type = "G0", Temperature = 6000, LuminosityMin = 1.1, LuminosityMax = 2.2, MainSequenceSpan = 7.7, SubGiantSpan = 1.2, GiantSpan = 0.7 } },
            new object[] { 1.15, new StellarEvolutionData { Type = "F9", Temperature = 6100, LuminosityMin = 1.4, LuminosityMax = 2.6, MainSequenceSpan = 6.7, SubGiantSpan = 1.0, GiantSpan = 0.6 } },
            new object[] { 1.20, new StellarEvolutionData { Type = "F8", Temperature = 6300, LuminosityMin = 1.7, LuminosityMax = 3.0, MainSequenceSpan = 5.9, SubGiantSpan = 0.9, GiantSpan = 0.6 } },
            new object[] { 1.25, new StellarEvolutionData { Type = "F7", Temperature = 6400, LuminosityMin = 2.1, LuminosityMax = 3.5, MainSequenceSpan = 5.2, SubGiantSpan = 0.8, GiantSpan = 0.5 } },
            new object[] { 1.30, new StellarEvolutionData { Type = "F6", Temperature = 6500, LuminosityMin = 2.5, LuminosityMax = 3.9, MainSequenceSpan = 4.6, SubGiantSpan = 0.7, GiantSpan = 0.4 } },
            new object[] { 1.35, new StellarEvolutionData { Type = "F5", Temperature = 6600, LuminosityMin = 3.1, LuminosityMax = 4.5, MainSequenceSpan = 4.1, SubGiantSpan = 0.6, GiantSpan = 0.4 } },
            new object[] { 1.40, new StellarEvolutionData { Type = "F4", Temperature = 6700, LuminosityMin = 3.7, LuminosityMax = 5.1, MainSequenceSpan = 3.7, SubGiantSpan = 0.6, GiantSpan = 0.4 } },
            new object[] { 1.45, new StellarEvolutionData { Type = "F3", Temperature = 6900, LuminosityMin = 4.3, LuminosityMax = 5.7, MainSequenceSpan = 3.3, SubGiantSpan = 0.5, GiantSpan = 0.3 } },
            new object[] { 1.50, new StellarEvolutionData { Type = "F2", Temperature = 7000, LuminosityMin = 5.1, LuminosityMax = 6.5, MainSequenceSpan = 3.0, SubGiantSpan = 0.5, GiantSpan = 0.3 } },
            new object[] { 1.60, new StellarEvolutionData { Type = "F0", Temperature = 7300, LuminosityMin = 6.7, LuminosityMax = 8.2, MainSequenceSpan = 2.5, SubGiantSpan = 0.4, GiantSpan = 0.2 } },
            new object[] { 1.70, new StellarEvolutionData { Type = "A9", Temperature = 7500, LuminosityMin = 8.6, LuminosityMax = 10, MainSequenceSpan = 2.1, SubGiantSpan = 0.3, GiantSpan = 0.2 } },
            new object[] { 1.80, new StellarEvolutionData { Type = "A7", Temperature = 7800, LuminosityMin = 11, LuminosityMax = 13, MainSequenceSpan = 1.8, SubGiantSpan = 0.3, GiantSpan = 0.2 } },
            new object[] { 1.90, new StellarEvolutionData { Type = "A6", Temperature = 8000, LuminosityMin = 13, LuminosityMax = 16, MainSequenceSpan = 1.5, SubGiantSpan = 0.2, GiantSpan = 0.1 } },
            new object[] { 2.00, new StellarEvolutionData { Type = "A5", Temperature = 8200, LuminosityMin = 16, LuminosityMax = 20, MainSequenceSpan = 1.3, SubGiantSpan = 0.2, GiantSpan = 0.1 } }
        };

        [Theory]
        [MemberData(nameof(BasicStellarTestData))]
        public void GetStellarData_ReturnsCorrectEntryBasedOnExactMass(double mass, StellarEvolutionData expectedData)
        {
            //Act
            StellarEvolutionData actualData = StellarCharacteristicsTables.GetStellarData(mass);
            //Assert
            Assert.Equal(expectedData.Type, actualData.Type);
            Assert.Equal(expectedData.Temperature, actualData.Temperature);
            Assert.Equal(expectedData.LuminosityMin, actualData.LuminosityMin);
            Assert.Equal(expectedData.LuminosityMax, actualData.LuminosityMax);
            Assert.Equal(expectedData.MainSequenceSpan, actualData.MainSequenceSpan);
            Assert.Equal(expectedData.SubGiantSpan, actualData.SubGiantSpan);
            Assert.Equal(expectedData.GiantSpan, actualData.GiantSpan);
        }

        [Fact]
        public void GetStellarData_ExactMassDoesNotExist_ReturnsClosestMassData()
        {
            // Arrange
            double mass = 1.02; // No existe exactamente en el diccionario

            // Act
            var result = StellarCharacteristicsTables.GetStellarData(mass);

            // Assert - Debería devolver el más cercano (1.0)
            Assert.Equal("G2", result.Type);
            Assert.Equal(5800, result.Temperature);
        }

        [Theory]
        [MemberData(nameof(BasicStellarTestData))]
        public void DetermineStarType_ReturnsCorrectStringBasedOnExactMass(double mass, StellarEvolutionData expectedData)
        {
            //Act
            string actual = StellarCharacteristicsTables.DetermineStarType(mass);
            //Assert
            Assert.Equal(expectedData.Type, actual);
        }

        [Theory]
        [InlineData(1.0, 5, "V")] // Secuencia principal
        [InlineData(1.0, 10.5, "IV")] // Subgigante
        [InlineData(1.0, 12.5, "III")] // Gigante
        [InlineData(1.0, 15, "D")] // Enana blanca
        [InlineData(0.3, 100, "V")] // Estrellas de larga vida siempre en secuencia principal
        public void DetermineLuminosityClass_ReturnsCorrectClass(double mass, double age, string expectedClass)
        {
            // Act
            string result = StellarCharacteristicsTables.DetermineLuminosityClass(mass, age);

            // Assert
            Assert.Equal(expectedClass, result);
        }

        [Theory]
        [InlineData("D", 0.001, 5800)] // Enana blanca
        [InlineData("V", 1.0, 5800)] // Secuencia principal
        [InlineData("IV", 5, 3200)] // Subgigante
        [InlineData("III", 25, 800)] // Gigante
        public void DetermineStarRadiusInAu_ReturnsValidRadius(string luminosityClass, double luminosity, int temperature)
        {

            // Act
            double actual = StellarCharacteristicsTables.DetermineStarRadiusInAu(luminosityClass, luminosity, temperature);

            // Assert
            if (luminosityClass == "D")
                Assert.Equal(0.000425, actual);
            else
            {
                double baseExpectedRadius = 155_000 * Math.Sqrt(luminosity) * Math.Pow(temperature, 2);
                Assert.True(actual > 0);
                Assert.InRange(actual, 0.9 * baseExpectedRadius, 1.1 * baseExpectedRadius);
            }
        }

        [Theory]
        [InlineData(1.0, 5)] // Estrella como el Sol a mitad de vida
        [InlineData(0.5, 10)] // Estrella más pequeña
        [InlineData(1.5, 1.5)] // Estrella más grande
        public void CalculateMainSequenceLuminosity_ReturnsValidLuminosity(double mass, double age)
        {
            // Arrange
            StellarEvolutionData data = StellarCharacteristicsTables.GetStellarData(mass);

            // Act
            double actual = StellarCharacteristicsTables.CalculateMainSequenceLuminosity(mass, age);

            //Assert
            // La luminosidad debería estar en un rango razonable considerando la variación del 10%
            if (data.LuminosityMax.HasValue)
                Assert.InRange(actual, data.LuminosityMin * 0.9, data.LuminosityMax.Value * 1.1);
            else
                Assert.InRange(actual, data.LuminosityMin * 0.9, data.LuminosityMin * 1.1);
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(1.5)]
        [InlineData(2.0)]
        public void CalculateMainSequenceTemperature_ReturnsTemperatureWithinRange(double mass)
        {
            // Arrange
            StellarEvolutionData data = StellarCharacteristicsTables.GetStellarData(mass);

            // Act
            int actual = StellarCharacteristicsTables.CalculateMainSequenceTemperature(mass);

            //Assert
            // La temperatura base más una variación de hasta ±100
            Assert.InRange(actual, data.Temperature - 100, data.Temperature + 100);
        }

        [Fact]
        public void CalculateSubGiantLuminosity_MassWithoutMaxLuminosity_ThrowsException()
        {
            // Arrange
            double mass = 0.3; // Estrella sin LuminosityMax

            // Act & Assert
            Assert.Throws<ArgumentException>(() => StellarCharacteristicsTables.CalculateSubGiantLuminosity(mass));
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(1.5)]
        public void CalculateSubGiantLuminosity_ReturnsMaxLuminosity(double mass)
        {
            //Arrange
            StellarEvolutionData data = StellarCharacteristicsTables.GetStellarData(mass);

            // Act
            double actual = StellarCharacteristicsTables.CalculateSubGiantLuminosity(mass);

            // Assert
            if (data.LuminosityMax.HasValue)
                Assert.Equal(data.LuminosityMax.Value, actual);
            else
                Assert.Fail();
        }

        [Theory]
        [InlineData(1.0, 11)] // Estrella solar en fase subgigante
        public void CalculateSubGiantTemperature_ReturnsTemperatureWithinRange(double mass, double age)
        {
            //Arrange
            StellarEvolutionData data = StellarCharacteristicsTables.GetStellarData(mass);

            // Act
            int actual = StellarCharacteristicsTables.CalculateSubGiantTemperature(mass, age);

            // Assert
            // La temperatura para subgigantes debería estar entre la temperatura original y 4800K
            Assert.InRange(actual, 4800 - 100, data.Temperature + 100);
        }

        [Fact]
        public void CalculateSubGiantTemperature_StarWithoutMainSequenceSpan_ThrowsException()
        {
            // Arrange
            double mass = 0.3; // Estrella sin MainSequenceSpan

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                StellarCharacteristicsTables.CalculateSubGiantTemperature(mass, 10));
        }

        [Fact]
        public void CalculateGiantTemperature_ReturnsTemperatureWithinRange()
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(2, -2)).Returns(0); // 0 para un valor medio

            // Act
            int actual = StellarCharacteristicsTables.CalculateGiantTemperature(mockDiceRoller.Object);

            // Assert
            Assert.Equal(3000, actual); // Con Roll(2, -2) = 0, debería ser exactamente 3000
            mockDiceRoller.Verify(d => d.Roll(2, -2), Times.Once);
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(1.5)]
        public void CalculateGiantLuminosity_ReturnsEnhancedLuminosity(double mass)
        {
            //Arrange
            StellarEvolutionData data = StellarCharacteristicsTables.GetStellarData(mass);

            // Act
            double actual = StellarCharacteristicsTables.CalculateGiantLuminosity(mass);

            // Assert
            // La luminosidad de la gigante debería ser aproximadamente 25 veces la luminosidad máxima
            // con una variación del 10%
            if (!data.LuminosityMax.HasValue)
                Assert.Fail();
            else
            {
                double expectedBase = 25 * data.LuminosityMax.Value;
                Assert.InRange(actual, expectedBase * 0.9, expectedBase * 1.1);
            }
        }

        [Fact]
        public void CalculateGiantLuminosity_StarWithoutMaxLuminosity_ThrowsException()
        {
            // Arrange
            double mass = 0.3; // Estrella sin LuminosityMax

            // Act & Assert
            Assert.Throws<ArgumentException>(() => StellarCharacteristicsTables.CalculateGiantLuminosity(mass));
        }

        [Fact]
        public void WhiteDwarfMass_ReturnsReasonableMass()
        {
            // Arrange
            var mockDiceRoller = new Mock<IDiceRoller>();
            mockDiceRoller.Setup(d => d.Roll(2, -2)).Returns(0);

            // Act
            double actual = StellarCharacteristicsTables.WhiteDwarfMass(mockDiceRoller.Object);

            // Assert
            Assert.Equal(0.9, actual); // Con Roll(2, -2) = 0, debería ser exactamente 0.9
            mockDiceRoller.Verify(d => d.Roll(2, -2), Times.Once);
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(1.5)]
        public void WhiteDwarfTemperature_ReturnsStellarTemperature(double mass)
        {
            // Act
            int actual = StellarCharacteristicsTables.WhiteDwarfTemperature(mass);

            // Assert
            StellarEvolutionData data = StellarCharacteristicsTables.GetStellarData(mass);
            Assert.Equal(data.Temperature, actual);
        }

        [Theory]
        [InlineData(1.0)]
        [InlineData(22.0)]
        [InlineData(37.0)]
        [InlineData(54.0)]
        [InlineData(-1.0)]
        [InlineData(-62.0)]
        [InlineData(-83.0)]
        [InlineData(0.1)]
        [InlineData(-0.1)]
        public void Apply10PercentVariationInEachDirection_ReturnsValueWithinRange(double baseValue)
        {
            // Act
            double actual = baseValue * StellarCharacteristicsTables.Apply10PercentVariationInEachDirection();

            // Assert
            if (baseValue >= 0.0)
                Assert.InRange(actual, 0.9 * baseValue, 1.1 * baseValue);
            else
                Assert.InRange(actual, 1.1 * baseValue, 0.9 * baseValue);
        }
    }
}
