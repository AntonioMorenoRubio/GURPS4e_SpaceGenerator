using GeneratorLibrary.Generators.Tables.Basic;
using GeneratorLibrary.Models.Basic;
using GeneratorLibrary.Tests.Utils;

namespace GeneratorLibrary.Tests.Generators.Tables.Basic
{
    public class TechLevelTablesTests
    {
        public const int MIN_TL = 0;
        public const int MAX_TL = 12;
        const int MAX_PRIMITIVE_TL = 6; // MAXIMUM(3d6) - 12 => 18 - 12 => 6. S91
        const int MIN_TL_IF_HABITABILITY_LESSEQUAL_THREE = 8;

        public static IEnumerable<object[]> GenerateTechStatusTestCases()
        {
            List<object[]> testCases = new List<object[]>();

            // Para cada tipo de asentamiento
            foreach (SettlementType settlementType in Enum.GetValues(typeof(SettlementType)))
                // Para cada estado de espacio reclamado
                foreach (bool isInClaimedSpace in new[] { true, false })
                    // Para cada valor de habitabilidad
                    for (int habitability = ResourceHabitabilityTablesTests.MINIMUM_HABITABILITY; habitability <= ResourceHabitabilityTablesTests.MAXIMUM_HABITABILITY; habitability++)
                    {
                        // Obtenemos el modificador según las reglas
                        int modifier = CalculateModifier(settlementType, isInClaimedSpace, habitability);

                        foreach (object[] roll in DiceRollerTests.Valid3dDiceRollValues())
                        {
                            int modifiedRoll = (int)roll[0] + modifier;
                            TechStatus expectedStatus = DetermineExpectedStatus(modifiedRoll);
                            testCases.Add(new object[] { settlementType, isInClaimedSpace, habitability, roll[0], expectedStatus });
                        }
                    }
            // Añadir casos especiales y extremos
            // Por ejemplo, valores límite de habitabilidad o casos donde se apliquen múltiples reglas

            // Filtrar casos redundantes o inválidos si es necesario
            return testCases.Distinct(new TestCaseComparer()).ToList();
        }

        private static int CalculateModifier(SettlementType type, bool isInClaimedSpace, int habitability)
        {
            int modifier = 0;

            // -10 si es un mundo natal fuera del espacio reclamado
            if (type == SettlementType.Homeworld && !isInClaimedSpace)
                modifier -= 10;
            // +1 si es un mundo natal o colonia con habitabilidad entre 4-6
            if ((type == SettlementType.Homeworld || type == SettlementType.Colony) &&
                     habitability >= 4 && habitability <= 6)
                modifier += 1;
            // +2 si es un mundo natal o colonia con habitabilidad ≤3
            if ((type == SettlementType.Homeworld || type == SettlementType.Colony) &&
                     habitability <= 3)
                modifier += 2;
            // +3 si es un puesto avanzado (outpost)
            if (type == SettlementType.Outpost)
                modifier += 3;

            return modifier;
        }

        private static TechStatus DetermineExpectedStatus(int modifiedRoll)
        {
            // Determinamos el estado tecnológico según la tabla
            if (modifiedRoll <= 3)
                return TechStatus.Primitive;
            else if (modifiedRoll == 4)
                return TechStatus.StandardMinus3;
            else if (modifiedRoll == 5)
                return TechStatus.StandardMinus2;
            else if (modifiedRoll == 6 || modifiedRoll == 7)
                return TechStatus.StandardMinus1;
            else if (modifiedRoll >= 8 && modifiedRoll <= 11)
                return TechStatus.Delayed;
            else if (modifiedRoll >= 12 && modifiedRoll <= 15)
                return TechStatus.Standard;
            else // modifiedRoll >= 16
                return TechStatus.Advanced;
        }

        [Theory]
        [MemberData(nameof(GenerateTechStatusTestCases))]
        public void DetermineTechStatus_ShouldReturnExpectedStatus(SettlementType type, bool isInClaimedSpace, int habitability, int roll, TechStatus expected)
        {
            // Arrange
            var settlementData = new SettlementData { Type = type, IsInClaimedSpace = isInClaimedSpace };

            // Act
            var result = TechLevelTables.DetermineTechStatus(settlementData, habitability, roll);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> PrimitiveWorldsWithOwnTL()
        {
            for (int i = 4; i <= ResourceHabitabilityTablesTests.MAXIMUM_HABITABILITY; i++)
                for (int j = 8; j <= MAX_TL; j++)
                    foreach (var roll in DiceRollerTests.Valid3dDiceRollValues())
                        yield return new object[] { TechStatus.Primitive, i, j, roll[0] };
        }

        [Theory]
        [MemberData(nameof(PrimitiveWorldsWithOwnTL))]
        public void DetermineTechLevel_PrimitiveWorlds_ReturnTLWithinValidRanges(TechStatus status, int habitability, int settingStandardTL, int roll)
        {
            // Act
            int result = TechLevelTables.DetermineTechLevel(status, habitability, settingStandardTL, roll);

            // Assert
            Assert.InRange(result, MIN_TL, MAX_PRIMITIVE_TL);
        }

        public static IEnumerable<object[]> WorldsThatNeedAtLeastTL8()
        {
            foreach (TechStatus status in Enum.GetValues(typeof(TechStatus)))
                for (int i = -2; i <= 3; i++)
                    for (int j = 8; j <= 12; j++)
                        yield return new object[] { status, i, j };
        }

        [Theory]
        [MemberData(nameof(WorldsThatNeedAtLeastTL8))]
        public void DetermineTechLevel_HabitabilityLessOrEqualsThree_ReturnTLWithinEightAndSettingStandardTL(TechStatus status, int habitability, int settingStandardTL)
        {
            // Act
            int result = TechLevelTables.DetermineTechLevel(status, habitability, settingStandardTL);

            // Assert
            Assert.InRange(result, MIN_TL_IF_HABITABILITY_LESSEQUAL_THREE, settingStandardTL);
        }

        public static IEnumerable<object[]> WorldsThatAdjustTLBasedOnStandardTL()
        {
            foreach (TechStatus status in Enum.GetValues(typeof(TechStatus)))
                if (status != TechStatus.Primitive)
                    for (int i = 4; i <= 10; i++)
                        for (int j = 8; j <= 12; j++)
                            switch (status)
                            {
                                case TechStatus.StandardMinus3:
                                    yield return new object[] { status, i, j, j - 3 };
                                    break;
                                case TechStatus.StandardMinus2:
                                    yield return new object[] { status, i, j, j - 2 };
                                    break;
                                case TechStatus.StandardMinus1:
                                    yield return new object[] { status, i, j, j - 1 };
                                    break;
                                default:
                                    yield return new object[] { status, i, j, j };
                                    break;
                            }
        }

        [Theory]
        [MemberData(nameof(WorldsThatAdjustTLBasedOnStandardTL))]
        public void DetermineTechLevel_Others_ShouldReturnExpectedTL(TechStatus status, int habitability, int settingStandardTL, int expectedTL)
        {
            // Act
            int result = TechLevelTables.DetermineTechLevel(status, habitability, settingStandardTL);

            // Assert
            Assert.Equal(expectedTL, result);
        }
    }

    // Clase para comparar casos de prueba y eliminar duplicados
    public class TestCaseComparer : IEqualityComparer<object[]>
    {
        public bool Equals(object[]? x, object[]? y)
        {
            if (x == null || y == null || x.Length != y.Length)
                return false;

            for (int i = 0; i < x.Length; i++)
            {
                if (!object.Equals(x[i], y[i]))
                    return false;
            }

            return true;
        }

        public int GetHashCode(object[] obj)
        {
            unchecked
            {
                int hash = 17;
                foreach (var item in obj)
                {
                    hash = hash * 23 + (item != null ? item.GetHashCode() : 0);
                }
                return hash;
            }
        }
    }
}
