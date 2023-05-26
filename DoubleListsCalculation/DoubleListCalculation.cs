namespace DoubleListsCalculation
{
    public static class DoubleListCalculation
    {
        public enum Operand
        {
            Average = 1,
            Sum = 2,
            Multiplication = 3,
            Different = 4
        }

        public enum CalculateListType
        {
            Column = 1,
            Row = 2
        }

        public static List<double> Calculate(this List<List<double>> list, Operand Action, CalculateListType Type, int? round)
        {
            List<double> result = new();

            if (list == null)
                return result;

            if (Type == CalculateListType.Column)
            {
                var transposed = list.SelectMany((row, i) => row.Select((value, j) => new { i, j, value }))
                .GroupBy(x => x.j, x => x.value)
                .Select(g => g.ToList())
                .ToList();
                result = Action switch
                {
                    Operand.Average => transposed.Select(col => Math.Round(col.Average(), round ?? 6)).ToList(),
                    Operand.Sum => transposed.Select(col => Math.Round(col.Sum(), round ?? 6)).ToList(),
                    _ => throw new ArgumentException("Invalid action", nameof(Action))
                };
            }
            else
            {
                result = Action switch
                {
                    Operand.Average => list.Select(row => Math.Round(row.Average(), round ?? 6)).ToList(),
                    Operand.Sum => list.Select(row => Math.Round(row.Sum(), round ?? 6)).ToList(),
                    _ => throw new ArgumentException("Invalid action", nameof(Action))
                };
            }

            return result;
        }

        public static double SumProduct(this List<double> MyList, List<double> SecondList, Operand innerOperand)
        {
            return innerOperand switch
            {
                Operand.Sum => MyList.Zip(SecondList, (a, b) => a + b).Sum(),
                Operand.Multiplication => MyList.Zip(SecondList, (a, b) => a * b).Sum(),
                Operand.Different => MyList.Zip(SecondList, (a, b) => a - b).Sum(),
            };
        }

        public static List<List<double>> Multiply(this List<List<double>> list, double number)
        {
            return list.Select(row => row.Select(value => value * number).ToList()).ToList();
        }
    }

}