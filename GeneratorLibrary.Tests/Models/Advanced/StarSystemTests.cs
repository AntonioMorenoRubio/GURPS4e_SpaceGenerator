using GeneratorLibrary.Models.Advanced;

namespace GeneratorLibrary.Tests.Models.Advanced
{
    public class StarSystemTests
    {
        [Fact]
        public void ToString_0Stars_ShouldReturnEmptyString()
        {
            //Arrange
            StarSystem starSystem = new StarSystem();

            //Act & Assert
            Assert.Equal("", starSystem.ToString());
        }

        [Fact]
        public void ToString_1Star_ShouldReturnOneStarString()
        {
            //Arrange
            StarSystem starSystem = new StarSystem();
            starSystem.Stars.Add(new Star());

            //Act & Assert
            Assert.Contains("Sistema con 1 estrella.", starSystem.ToString());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void ToString_NStars_ShouldReturnNStarsString(int stars)
        {
            //Arrange
            StarSystem starSystem = new StarSystem();
            for (int i = 0; i < stars; i++)
            {
                starSystem.Stars.Add(new Star());
            }

            //Act & Assert
            Assert.Contains($"Sistema con {starSystem.Stars.Count} estrellas.", starSystem.ToString());
        }
    }
}
