using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class ClimateTablesTests
    {
        [Theory]
        [InlineData(WorldSize.Special, WorldSubType.AsteroidBelt, 140d, 500d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice, 80d, 140d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur, 80d, 140d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock, 140d, 500d)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean, 50d, 80d)]
        [InlineData(WorldSize.Small, WorldSubType.Ice, 80d, 140d)]
        [InlineData(WorldSize.Small, WorldSubType.Rock, 140d, 500d)]
        [InlineData(WorldSize.Standard, WorldSubType.Hadean, 50d, 80d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia, 140d, 215d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ice, 80d, 230d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 250d, 340d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 250d, 340d)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse, 500d, 950d)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian, 500d, 950d)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia, 140d, 215d)]
        [InlineData(WorldSize.Large, WorldSubType.Ice, 80d, 230d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 250d, 340d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 250d, 340d)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse, 500d, 950d)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian, 500d, 950d)]
        public void GenerateAverageSurfaceTemperatureInKelvinsByWorldType_ShouldReturnValidRange(WorldSize size, WorldSubType subType, double minExpected, double maxExpected)
        {
            // Act
            double result = ClimateTables.GenerateAverageSurfaceTemperatureInKelvinsByWorldType(size, subType);

            // Assert
            Assert.InRange(result, minExpected, maxExpected);
        }

        [Theory]
        [InlineData(243.99d, ClimateType.Frozen)]   // Caso límite inferior para "Frozen"
        [InlineData(244d, ClimateType.VeryCold)]    // Límite inferior de "VeryCold"
        [InlineData(254.99d, ClimateType.VeryCold)] // Justo antes de "Cold"
        [InlineData(255d, ClimateType.Cold)]        // Límite inferior de "Cold"
        [InlineData(265.99d, ClimateType.Cold)]     // Justo antes de "Chilly"
        [InlineData(266d, ClimateType.Chilly)]      // Límite inferior de "Chilly"
        [InlineData(277.99d, ClimateType.Chilly)]   // Justo antes de "Cool"
        [InlineData(278d, ClimateType.Cool)]        // Límite inferior de "Cool"
        [InlineData(288.99d, ClimateType.Cool)]     // Justo antes de "Normal"
        [InlineData(289d, ClimateType.Normal)]      // Límite inferior de "Normal"
        [InlineData(299.99d, ClimateType.Normal)]   // Justo antes de "Warm"
        [InlineData(300d, ClimateType.Warm)]        // Límite inferior de "Warm"
        [InlineData(310.99d, ClimateType.Warm)]     // Justo antes de "Tropical"
        [InlineData(311d, ClimateType.Tropical)]    // Límite inferior de "Tropical"
        [InlineData(321.99d, ClimateType.Tropical)] // Justo antes de "Hot"
        [InlineData(322d, ClimateType.Hot)]         // Límite inferior de "Hot"
        [InlineData(332.99d, ClimateType.Hot)]      // Justo antes de "VeryHot"
        [InlineData(333d, ClimateType.VeryHot)]     // Límite inferior de "VeryHot"
        [InlineData(344d, ClimateType.VeryHot)]    // Límite superior de "VeryHot"
        [InlineData(344.01d, ClimateType.Infernal)]    // Caso límite inferior para "Infernal"
        public void GetClimateTypeBasedOnKelvinTemperature_ReturnsCorrectClimateType(double kelvins, ClimateType expectedClimateType)
        {
            // Act
            var result = ClimateTables.GetClimateTypeBasedOnKelvinTemperature(kelvins);

            // Assert
            Assert.Equal(expectedClimateType, result);
        }

        [Theory]
        [InlineData(WorldSize.Special, WorldSubType.AsteroidBelt, 0d, 0.97d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice, 0d, 0.86d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock, 0d, 0.97d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur, 0d, 0.77d)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean, 0d, 0.67d)]
        [InlineData(WorldSize.Small, WorldSubType.Ice, 0d, 0.93d)]
        [InlineData(WorldSize.Small, WorldSubType.Rock, 0d, 0.96d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia, 0d, 0.84d)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia, 0d, 0.84d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ice, 0d, 0.86d)]
        [InlineData(WorldSize.Large, WorldSubType.Ice, 0d, 0.86d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 1d, 0.95d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 30d, 0.92d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 86d, 0.88d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 95d, 0.84d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 1d, 0.95d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 25d, 0.92d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 63d, 0.88d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 98d, 0.84d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 1d, 0.95d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 30d, 0.92d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 86d, 0.88d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 95d, 0.84d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 1d, 0.95d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 25d, 0.92d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 63d, 0.88d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 98d, 0.84d)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse, 0d, 0.77d)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse, 0d, 0.77d)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian, 0d, 0.97d)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian, 0d, 0.97d)]
        public void GetAbsorptionFactor_ReturnsCorrectFactor(WorldSize size, WorldSubType subType, double hydrographicCoverage, double expectedFactor)
        {
            // Act
            double result = ClimateTables.GetAbsorptionFactor(size, subType, hydrographicCoverage);

            // Assert
            Assert.Equal(expectedFactor, result);
        }

        [Theory]
        [InlineData(10d, 0.95d)] // hydrographicCoverage < 21d
        [InlineData(30d, 0.92d)] // hydrographicCoverage >= 21d and < 51d
        [InlineData(70d, 0.88d)] // hydrographicCoverage >= 51d and < 91d
        [InlineData(95d, 0.84d)] // hydrographicCoverage >= 91d
        public void GetOceanGardenAbsorptionFactor_ReturnsCorrectFactor(double hydrographicCoverage, double expectedAbsorptionFactor)
        {
            // Act
            double result = ClimateTables.GetOceanGardenAbsorptionFactor(hydrographicCoverage);

            // Assert
            Assert.Equal(expectedAbsorptionFactor, result);
        }

        [Theory]
        [InlineData(WorldSize.Special, WorldSubType.AsteroidBelt, 0d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice, 0d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock, 0d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur, 0d)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean, 0d)]
        [InlineData(WorldSize.Small, WorldSubType.Ice, 0.1d)]
        [InlineData(WorldSize.Small, WorldSubType.Rock, 0d)]
        [InlineData(WorldSize.Standard, WorldSubType.Hadean, 0d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia, 0.20d)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia, 0.20d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ice, 0.20d)]
        [InlineData(WorldSize.Large, WorldSubType.Ice, 0.20d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 0.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 0.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 0.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 0.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse, 2d)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse, 2d)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian, 0d)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian, 0d)]
        public void GetGreenhouseFactor_ReturnsCorrectFactor(WorldSize size, WorldSubType subType, double expectedFactor)
        {
            // Act
            double result = ClimateTables.GetGreenhouseFactor(size, subType);

            // Assert
            Assert.Equal(expectedFactor, result);
        }

        [Theory]
        [InlineData(WorldSize.Special, WorldSubType.AsteroidBelt, 1d, 0d, 0.97d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Ice, 1d, 0d, 0.86d * 1.01d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Rock, 1d, 0d, 0.97d)]
        [InlineData(WorldSize.Tiny, WorldSubType.Sulfur, 1d, 0d, 0.77d)]
        [InlineData(WorldSize.Small, WorldSubType.Hadean, 1d, 0d, 0.67d * 1.01d)]
        [InlineData(WorldSize.Small, WorldSubType.Ice, 1d, 0d, 0.93d * 1.1d)]
        [InlineData(WorldSize.Small, WorldSubType.Rock, 1d, 0d, 0.96d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ammonia, 1d, 0d, 0.84d * 1.2d)]
        [InlineData(WorldSize.Large, WorldSubType.Ammonia, 1d, 0d, 0.84d * 1.2d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ice, 1d, 0d, 0.86d * 1.2d)]
        [InlineData(WorldSize.Large, WorldSubType.Ice, 1d, 0d, 0.86d * 1.2d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 1d, 1d, 0.95d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 1d, 30d, 0.92d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 1d, 86d, 0.88d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Ocean, 1d, 95d, 0.84d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 1d, 1d, 0.95d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 1d, 25d, 0.92d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 1d, 63d, 0.88d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Garden, 1d, 98d, 0.84d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 1d, 1d, 0.95d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 1d, 30d, 0.92d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 1d, 86d, 0.88d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Ocean, 1d, 95d, 0.84d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 1d, 1d, 0.95d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 1d, 25d, 0.92d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 1d, 63d, 0.88d * 1.16d)]
        [InlineData(WorldSize.Large, WorldSubType.Garden, 1d, 98d, 0.84d * 1.16d)]
        [InlineData(WorldSize.Standard, WorldSubType.Greenhouse, 1d, 0d, 0.77d * 3d)]
        [InlineData(WorldSize.Large, WorldSubType.Greenhouse, 1d, 0d, 0.77d * 3d)]
        [InlineData(WorldSize.Standard, WorldSubType.Chthonian, 1d, 0d, 0.97d)]
        [InlineData(WorldSize.Large, WorldSubType.Chthonian, 1d, 0d, 0.97d)]
        public void GenerateBlackbodyCorrection_ReturnsCorrectCorrection(WorldSize size, WorldSubType subType, double atmosphereMass, double hydrographicCoverage, double expectedCorrection)
        {
            // Act
            double result = ClimateTables.GenerateBlackbodyCorrection(size, subType, atmosphereMass, hydrographicCoverage);

            // Assert
            Assert.Equal(expectedCorrection, result, 1);
        }
    }
}
