using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class TechLevelTablesTests
    {
        const int MIN_TL = 0;
        const int MAX_PRIMITIVE_TL = 6; // MAXIMUM(3d6) - 12 => 18 - 12 => 6. S91
        const int MIN_HABITABILITY_LESSEQUAL_THREE = 8;


        [Theory]
        // Caso 1: Homeworld fuera de espacio reclamado (modificador -10)
        [InlineData(SettlementType.Homeworld, false, 5, 10, TechStatus.Primitive)]
        [InlineData(SettlementType.Homeworld, false, 8, 10, TechStatus.Primitive)]

        // Caso 2: Homeworld en espacio reclamado, habitabilidad 4-6 (modificador +1)
        [InlineData(SettlementType.Homeworld, true, 4, 7, TechStatus.Delayed)] // (7 + 1 = 8) Standard (Delayed)
        [InlineData(SettlementType.Homeworld, true, 5, 11, TechStatus.Standard)] // (11 + 1 = 12) Standard
        [InlineData(SettlementType.Homeworld, true, 6, 15, TechStatus.Advanced)] // (15 + 1 = 16) Standard (Advanced)

        // Caso 3: Homeworld en espacio reclamado, habitabilidad ≤3 (modificador +2)
        [InlineData(SettlementType.Homeworld, true, 1, 2, TechStatus.StandardMinus3)] // (2 + 2 = 4) Standard - 3
        [InlineData(SettlementType.Homeworld, true, 1, 3, TechStatus.StandardMinus2)] // (3 + 2 = 5) Standard - 2
        [InlineData(SettlementType.Homeworld, true, 1, 5, TechStatus.StandardMinus1)] // (5 + 2 = 7) Standard - 1
        [InlineData(SettlementType.Homeworld, true, 3, 6, TechStatus.Delayed)] // (6 + 2 = 8) Standard (Delayed)
        [InlineData(SettlementType.Homeworld, true, 3, 9, TechStatus.Delayed)] // (9 + 2 = 11) Standard (Delayed)
        [InlineData(SettlementType.Homeworld, true, 2, 10, TechStatus.Standard)] // (10 + 2 = 12) Standard
        [InlineData(SettlementType.Homeworld, true, 2, 13, TechStatus.Standard)] // (13 + 2 = 15) Standard
        [InlineData(SettlementType.Homeworld, true, 2, 14, TechStatus.Advanced)] // (14 + 2 = 16) Standard (Advanced)
        [InlineData(SettlementType.Homeworld, true, 2, 17, TechStatus.Advanced)] // (17 + 2 = 19) Standard (Advanced)

        // Caso 4: Outpost (modificador +3)
        [InlineData(SettlementType.Outpost, true, 5, 5, TechStatus.Delayed)] // (5 + 4 = 9) Standard (Delayed)
        [InlineData(SettlementType.Outpost, true, 5, 8, TechStatus.Delayed)] // (8 + 4 = 12) Standard
        [InlineData(SettlementType.Outpost, true, 5, 15, TechStatus.Advanced)] // (15 + 3 = 18) Standard (Advanced)

        // Caso 5: Colonia con habitabilidad > 6 (sin modificador)
        [InlineData(SettlementType.Homeworld, true, 7, 10, TechStatus.Delayed)] // (10 + 0 = 10) TL (Delayed)
        [InlineData(SettlementType.Colony, true, 7, 10, TechStatus.Delayed)] // (10 + 0 = 10) TL (Delayed)

        // Caso 6: Tira alta (16+) siempre es Advanced
        [InlineData(SettlementType.Colony, true, 7, 16, TechStatus.Advanced)]
        [InlineData(SettlementType.Homeworld, true, 5, 17, TechStatus.Advanced)]
        [InlineData(SettlementType.Outpost, true, 3, 15, TechStatus.Advanced)]
        public void DetermineTechStatus_ShouldReturnExpectedStatus(SettlementType type, bool isInClaimedSpace, int habitability, int roll, TechStatus expected)
        {
            // Arrange
            var settlementData = new SettlementData { Type = type, IsInClaimedSpace = isInClaimedSpace };

            // Act
            var result = TechLevelTables.DetermineTechStatus(settlementData, habitability, roll);

            // Assert
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(TechStatus.Primitive, 4, 10, 3)]
        [InlineData(TechStatus.Primitive, 5, 10, 3)]
        [InlineData(TechStatus.Primitive, 6, 10, 3)]
        [InlineData(TechStatus.Primitive, 7, 10, 3)]
        [InlineData(TechStatus.Primitive, 8, 10, 3)]
        [InlineData(TechStatus.Primitive, 9, 10, 3)]
        [InlineData(TechStatus.Primitive, 10, 10, 3)]
        [InlineData(TechStatus.Primitive, 4, 10, 18)]
        [InlineData(TechStatus.Primitive, 5, 10, 18)]
        [InlineData(TechStatus.Primitive, 6, 10, 18)]
        [InlineData(TechStatus.Primitive, 7, 10, 18)]
        [InlineData(TechStatus.Primitive, 8, 10, 18)]
        [InlineData(TechStatus.Primitive, 9, 10, 18)]
        [InlineData(TechStatus.Primitive, 10, 10, 18)]
        public void DetermineTechLevel_PrimitiveWorlds_ReturnTLWithinValidRanges(TechStatus status, int habitability, int settingStandardTL, int roll)
        {
            // Act
            int result = TechLevelTables.DetermineTechLevel(status, habitability, settingStandardTL, roll);

            // Assert
            Assert.InRange(result, MIN_TL, MAX_PRIMITIVE_TL);
        }


        [Theory]
        [InlineData(TechStatus.Primitive, 2, 10)]
        [InlineData(TechStatus.StandardMinus3, 3, 10)]
        [InlineData(TechStatus.StandardMinus2, 2, 10)]
        [InlineData(TechStatus.StandardMinus1, 1, 10)]
        [InlineData(TechStatus.Delayed, 0, 10)]
        [InlineData(TechStatus.Standard, -1, 10)]
        [InlineData(TechStatus.Advanced, -2, 10)]
        public void DetermineTechLevel_HabitabilityLessOrEqualsThree_ReturnTLWithinEightAndSettingStandardTL(TechStatus status, int habitability, int settingStandardTL)
        {
            // Act
            int result = TechLevelTables.DetermineTechLevel(status, habitability, settingStandardTL);

            // Assert
            Assert.InRange(result, MIN_HABITABILITY_LESSEQUAL_THREE, settingStandardTL);
        }

        [Theory]
        [InlineData(TechStatus.StandardMinus3, 5, 10, 7)]
        [InlineData(TechStatus.StandardMinus2, 5, 10, 8)]
        [InlineData(TechStatus.StandardMinus1, 5, 10, 9)]
        [InlineData(TechStatus.Delayed, 5, 10, 10)]
        [InlineData(TechStatus.Standard, 5, 10, 10)]
        [InlineData(TechStatus.Advanced, 5, 10, 10)]
        public void DetermineTechLevel_Others_ShouldReturnExpectedTL(TechStatus status, int habitability, int settingStandardTL, int expectedTL)
        {
            // Act
            int result = TechLevelTables.DetermineTechLevel(status, habitability, settingStandardTL);

            // Assert
            Assert.Equal(expectedTL, result);
        }
    }
}
