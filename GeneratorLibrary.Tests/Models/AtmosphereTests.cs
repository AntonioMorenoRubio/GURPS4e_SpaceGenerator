
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Models
{
    public class AtmosphereTests
    {
        [Fact]
        public void Atmosphere_With_ShouldCreateNewInstanceWithModifiedValues()
        {
            // Arrange
            var original = new Atmosphere
            {
                Mass = 1.2,
                Composition = new List<string> { "Nitrogen", "Oxygen" },
                Characteristics = new List<AtmosphereCharacteristic> { AtmosphereCharacteristic.MildlyToxic },
                MarginalAtmosphere = MarginalAtmosphere.HighCarbonDioxide
            };

            // Act
            var modified = original with { Mass = 2.0 };

            // Assert
            Assert.Equal(1.2, original.Mass); // El original no cambia
            Assert.Equal(2.0, modified.Mass); // El nuevo tiene el valor actualizado
            Assert.NotSame(original, modified); // Deben ser instancias distintas
        }
    }
}
