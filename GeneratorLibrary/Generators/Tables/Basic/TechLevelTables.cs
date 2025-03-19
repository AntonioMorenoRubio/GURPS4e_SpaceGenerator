using GeneratorLibrary.Models.Basic;

namespace GeneratorLibrary.Generators.Tables.Basic
{
    public static class TechLevelTables
    {
        public static TechStatus DetermineTechStatus(SettlementData settlementData, int habitability, int roll)
        {
            List<int> modifiers = new();

            // Aplicar modificadores según el tipo de asentamiento
            if (settlementData.Type is SettlementType.Homeworld && !settlementData.IsInClaimedSpace)
                modifiers.Add(-10);
            if (settlementData.Type is SettlementType.Homeworld && habitability is >= 4 and <= 6)
                modifiers.Add(1);
            if (settlementData.Type is SettlementType.Homeworld && habitability is <= 3)
                modifiers.Add(2);
            if (settlementData.Type is SettlementType.Outpost)
                modifiers.Add(3);

            int finalRoll = roll + modifiers.Sum();

            // Devolver TLStatus base a partir de la tabla
            return finalRoll switch
            {
                <= 3 => TechStatus.Primitive,           // Primitivo
                4 => TechStatus.StandardMinus3,         // TL-3
                5 => TechStatus.StandardMinus2,         // TL-2
                6 or 7 => TechStatus.StandardMinus1,    // TL-1
                >= 8 and <= 11 => TechStatus.Delayed,   // Standard (Delayed)
                >= 16 => TechStatus.Advanced,           // Standard (Advanced)
                _ => TechStatus.Standard                // Standard
            };
        }

        public static int DetermineTechLevel(TechStatus status, int habitability, int settingStandardTL, int roll = 3)
        {
            var techLevel = status switch
            {
                // Si es primitivo, calcular TL con roll-12 (mínimo TL0)
                TechStatus.Primitive => Math.Clamp(roll - 12, 0, settingStandardTL),
                TechStatus.StandardMinus3 => settingStandardTL - 3,
                TechStatus.StandardMinus2 => settingStandardTL - 2,
                TechStatus.StandardMinus1 => settingStandardTL - 1,
                //Standard (Normal, Delayed o Advanced)
                _ => settingStandardTL,
            };

            // Un mundo con habitabilidad 3 o menos, sin importar lo anterior, debe ser TL8 mínimo.
            if (habitability <= 3)
                techLevel = Math.Max(techLevel, 8);

            return techLevel;
        }
    }
}
