namespace GeneratorLibrary.Models
{
    public record ControlRating
    {
        public int CR { get; set; }
        public (int Min, int Max) MinMaxCR { get; set; }
        public List<(int, int)> CRList { get; set; } = new();
    }
}
