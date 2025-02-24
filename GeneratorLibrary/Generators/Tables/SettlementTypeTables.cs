using GeneratorLibrary.Models;

namespace GeneratorLibrary.Generators.Tables
{
    public static class SettlementTypeTables
    {
        public static SettlementType DetermineSettlementType(int affinity, bool isClaimedSpace, bool isHomeworld = false)
        {
            // Homeworlds deben definirse manualmente
            if (isHomeworld)
                return SettlementType.Homeworld;

            // Uninhabited: Mundo fuera del espacio reclamado
            if (!isClaimedSpace)
                return SettlementType.Uninhabited;

            // Colony: Mundo en espacio reclamado con afinidad positiva
            if (affinity > 0)
                return SettlementType.Colony;

            // Outpost: Mundo en espacio reclamado, pero sin afinidad para colonización
            return SettlementType.Outpost;
        }
    }
}
