using GeneratorLibrary.Generators.Tables;
using GeneratorLibrary.Models;

namespace GeneratorLibrary.Tests.Generators.Tables
{
    public class SettlementTypeTablesTests
    {
        public static IEnumerable<object[]> SettlementTestData => new List<object[]>
        {
            new object[] { 5 , true, false, SettlementType.Colony }, // Mundo colonizable

            new object[] { -1 , true, false, SettlementType.Outpost }, // Mundo inhóspito pero en espacio reclamado

            new object[] { 3 , false, false, SettlementType.Uninhabited }, // Mundo en espacio no reclamado

            new object[] { 2, true, true, SettlementType.Homeworld }, // Homeworld definido manualmente

            new object[] { -3, false, false, SettlementType.Uninhabited } // Mundo en espacio no reclamado y no apto
        };

        [Theory]
        [MemberData(nameof(SettlementTestData))]
        public void DetermineSettlementType_ShouldReturnExpected(int affinity, bool isClaimedSpace, bool isHomeworld, SettlementType expected)
        {
            // Act
            SettlementType result = SettlementDataTables.DetermineSettlementType(affinity, isClaimedSpace, isHomeworld);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
