﻿using GeneratorLibrary.Utils;
using Moq;

namespace GeneratorLibrary.Tests.Utils;

public class DiceRollerTests
{
    public static IEnumerable<object[]> Valid1dDiceRollValues()
    {
        for (int diceRoll = 1; diceRoll <= 6; diceRoll++)
        {
            yield return new object[] { diceRoll };
        }
    }
    public static IEnumerable<object[]> Valid2dDiceRollValues()
    {
        for (int diceRoll = 2; diceRoll <= 12; diceRoll++)
        {
            yield return new object[] { diceRoll };
        }
    }

    public static IEnumerable<object[]> Valid3dDiceRollValues()
    {
        for (int diceRoll = 3; diceRoll <= 18; diceRoll++)
        {
            yield return new object[] { diceRoll };
        }
    }

    public static IEnumerable<object[]> Invalid3dDiceRollValues() => new List<object[]>
    {
        new object[] { 2 },
        new object[] { 1 },
        new object[] { 0 },
        new object[] { -1 },
        new object[] { -2 },
        new object[] { 19 },
        new object[] { 20 },
        new object[] { 21 },
        new object[] { 22 },
        new object[] { int.MinValue },
        new object[] { int.MaxValue }
    };

    public static IEnumerable<object[]> AllTestDiceRolls() => Valid3dDiceRollValues().Union(Invalid3dDiceRollValues());


    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void Roll_Default3d6_ShouldReturnValueBetween3And18_MultipleTimes(int rolls)
    {
        // Arrange
        DiceRoller roller = new DiceRoller();

        for (int i = 0; i < rolls; i++)
        {
            // Act
            var result = roller.Roll();

            // Assert
            Assert.InRange(result, 3, 18);
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public void Roll_MultipleTimes_ShouldReturnDifferentValues(int repetitions)
    {
        // Arrange
        DiceRoller roller = new DiceRoller();
        List<int> results = new();

        // Act
        for (int i = 0; i < repetitions; i++)
        {
            results.Add(roller.Roll());
        }

        // Assert
        Assert.True(results.Distinct().Count() > 1);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void Roll_Nd6_ShouldReturnValueWithinValidRange(int numberOfDice)
    {
        // Arrange
        DiceRoller roller = new DiceRoller();

        // Act
        var result = roller.Roll(numberOfDice);

        // Assert
        Assert.InRange(result, numberOfDice, numberOfDice * 6);
    }

    [Theory]
    [InlineData(3, 0)]
    [InlineData(3, +2)]
    [InlineData(3, -3)]
    [InlineData(3, +10)]
    [InlineData(3, -10)]
    [InlineData(4, +5)]
    [InlineData(2, -2)]
    [InlineData(1, new int[] { -3, +2 })]
    [InlineData(5, new int[] { +4, -1 })]
    [InlineData(6, new int[] { -2, -2, +5 })]
    [InlineData(3, int.MinValue)]
    [InlineData(3, int.MaxValue)]
    public void Roll_Nd6_WithModifier_ShouldReturnCorrectRange(int numberOfDice, params int[] modifier)
    {
        // Arrange
        DiceRoller roller = new DiceRoller();

        // Act
        var result = roller.Roll(numberOfDice, modifier);

        // Assert
        Assert.InRange(result, numberOfDice + modifier.Sum(), numberOfDice * 6 + modifier.Sum());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(22)]
    [InlineData(100)]
    [InlineData(5000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Roll_WithFixedSeed_ShouldBeDeterministic(int seed)
    {
        // Arrange
        DiceRoller roller;
        int[] results = new int[2];

        // Act
        for (int i = 0; i <= 1; i++)
        {
            roller = new DiceRoller(seed);
            results[i] = roller.Roll();
        }


        // Assert
        Assert.Equal(results[0], results[1]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-100)]
    [InlineData(-1000)]
    [InlineData(int.MinValue)]
    public void Roll_InvalidNumberOfDice_ShouldThrowArgumentException(int numberOfDice)
    {
        // Arrange
        DiceRoller roller = new DiceRoller();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => roller.Roll(numberOfDice));
    }

    [Theory]
    [MemberData(nameof(AllTestDiceRolls))]
    public void Roll_MockedDiceRoller_ShouldReturnValueToMock(int valueToMock)
    {
        //Arrange
        Mock<IDiceRoller> mockIDiceRoller = new Mock<IDiceRoller>();
        mockIDiceRoller.Setup(d => d.Roll(It.IsAny<int>(), It.IsAny<int[]>()))
                      .Returns(valueToMock);
        IDiceRoller IDiceRoller = mockIDiceRoller.Object;

        //Act
        int actual = IDiceRoller.Roll();

        //Assert
        Assert.Equal(valueToMock, actual);
        mockIDiceRoller.Verify(r => r.Roll(It.IsAny<int>(), It.IsAny<int[]>()), Times.Once());
    }
}
