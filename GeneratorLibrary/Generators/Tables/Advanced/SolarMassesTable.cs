namespace GeneratorLibrary.Generators.Tables.Advanced
{
    public static class SolarMassesTable
    {
        private static Dictionary<(int, int), double> _massTable = new Dictionary<(int, int), double>();

        static SolarMassesTable()
        {
            PopulateMassTable();
        }

        private static void PopulateMassTable()
        {
            // inicializa la tabla con los valores correspondientes
            for (int i = 3; i <= 18; i++)
            {
                for (int j = 3; j <= 18; j++)
                {
                    // lógica para generar el valor de la tabla en la posición (i, j)
                    switch (i)
                    {
                        case 3:
                            if (j >= 3 && j <= 10)
                                _massTable.Add((i, j), 2.00);
                            else if (j >= 11 && j <= 18)
                                _massTable.Add((i, j), 1.90);
                            break;
                        case 4:
                            if (j >= 3 && j <= 8)
                                _massTable.Add((i, j), 1.80);
                            else if (j >= 9 && j <= 11)
                                _massTable.Add((i, j), 1.70);
                            else if (j >= 12 && j <= 18)
                                _massTable.Add((i, j), 1.60);
                            break;
                        case 5:
                            if (j >= 3 && j <= 7)
                                _massTable.Add((i, j), 1.50);
                            else if (j >= 8 && j <= 10)
                                _massTable.Add((i, j), 1.45);
                            else if (j >= 11 && j <= 12)
                                _massTable.Add((i, j), 1.40);
                            else if (j >= 13 && j <= 18)
                                _massTable.Add((i, j), 1.35);
                            break;
                        case 6:
                            if (j >= 3 && j <= 7)
                                _massTable.Add((i, j), 1.30);
                            else if (j >= 8 && j <= 9)
                                _massTable.Add((i, j), 1.25);
                            else if (j == 10)
                                _massTable.Add((i, j), 1.20);
                            else if (j >= 11 && j <= 12)
                                _massTable.Add((i, j), 1.15);
                            else if (j >= 13 && j <= 18)
                                _massTable.Add((i, j), 1.10);
                            break;
                        case 7:
                            if (j >= 3 && j <= 7)
                                _massTable.Add((i, j), 1.05);
                            else if (j >= 8 && j <= 9)
                                _massTable.Add((i, j), 1.00);
                            else if (j == 10)
                                _massTable.Add((i, j), 0.95);
                            else if (j >= 11 && j <= 12)
                                _massTable.Add((i, j), 0.90);
                            else if (j >= 13 && j <= 18)
                                _massTable.Add((i, j), 0.85);
                            break;
                        case 8:
                            if (j >= 3 && j <= 7)
                                _massTable.Add((i, j), 0.80);
                            else if (j >= 8 && j <= 9)
                                _massTable.Add((i, j), 0.75);
                            else if (j == 10)
                                _massTable.Add((i, j), 0.70);
                            else if (j >= 11 && j <= 12)
                                _massTable.Add((i, j), 0.65);
                            else if (j >= 13 && j <= 18)
                                _massTable.Add((i, j), 0.60);
                            break;
                        case 9:
                            if (j >= 3 && j <= 8)
                                _massTable.Add((i, j), 0.55);
                            else if (j >= 9 && j <= 11)
                                _massTable.Add((i, j), 0.50);
                            else if (j >= 12 && j <= 18)
                                _massTable.Add((i, j), 0.45);
                            break;
                        case 10:
                            if (j >= 3 && j <= 8)
                                _massTable.Add((i, j), 0.40);
                            else if (j >= 9 && j <= 11)
                                _massTable.Add((i, j), 0.35);
                            else if (j >= 12 && j <= 18)
                                _massTable.Add((i, j), 0.30);
                            break;
                        case 11:
                            _massTable.Add((i, j), 0.25);
                            break;
                        case 12:
                            _massTable.Add((i, j), 0.20);
                            break;
                        case 13:
                            _massTable.Add((i, j), 0.15);
                            break;
                        default:
                            _massTable.Add((i, j), 0.10);
                            break;
                    }
                }
            }
        }

        public static double GetPrimaryStarMass(int roll1, int roll2) => _massTable[(roll1, roll2)];

        public static double GetCompanionStarMass(double primaryStarMass)
        {
            double _MINIMUM_MASS = 0.10;
            double _TOLERANCE = 1e-6;

            if (Math.Abs(_MINIMUM_MASS - primaryStarMass) < _TOLERANCE)
                return _MINIMUM_MASS;

            int stepRoll = DiceRoller.Instance.Roll(1, -1);

            if (stepRoll == 0)
                return primaryStarMass;

            stepRoll = DiceRoller.Instance.Roll(stepRoll);

            KeyValuePair<(int, int), double> key = _massTable.FirstOrDefault(
                x => Math.Abs(x.Value - primaryStarMass) < _TOLERANCE);

            for (int i = 0; i < stepRoll; i++)
            {
                key = _massTable.FirstOrDefault(x => x.Value < key.Value);
                if (key.Key.Item1 >= 14)
                    return 0.10;    //Early return, we reached bottom of Stellar Mass Table (S101)
            }

            return key.Value;
        }
    }
}
