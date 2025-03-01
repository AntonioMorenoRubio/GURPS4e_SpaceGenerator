namespace GeneratorLibrary.Utils
{
    public static class MathExtensions
    {
        public static decimal RoundToSignificantFigures(this decimal value, int figures)
        {
            if (value == 0 || figures < 1)
                return 0;

            double dValue = Convert.ToDouble(value);
            double dScale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(dValue))) + 1 - figures);

            decimal scale = Convert.ToDecimal(dScale);


            return Math.Round(value / scale) * scale;
        }
    }

}
