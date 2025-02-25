namespace GeneratorLibrary.Models
{
    public record TechLevel
    {
        public int TL { get; set; }
        public TechStatus Status { get; set; }
    }

    public enum TechStatus
    {
        Primitive,      // Cortado del comercio interestelar. Habitantes tienen Low TL.
        StandardMinus3, // Tres TL retrasado con respecto al nivel estándar.
        StandardMinus2, // Dos TL retrasado con respecto al nivel estándar.
        StandardMinus1, // Un TL retrasado con respecto al nivel estándar.
        Delayed,        // Standard pero con tecnología menos eficiente. +10% coste.
        Standard,       // Normal para la ambientación.
        Advanced        // Equipos más avanzados, compactos o estilizados. -10% coste.
    }
}
