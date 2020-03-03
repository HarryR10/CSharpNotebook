namespace Calculator
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Exceptions;

    public class Parser : IParser
    {
        Regex _regex = new Regex(
            @"^\s*\S+(\s+\-?((\d+\.\d+)|(\d+)))+");

        public Operation Parse(string inputString)
        {
            var match = _regex.Match(inputString);
            if (match.Success)
            {
                string[] afterSplit = match.Value.Split(" ",
                    StringSplitOptions.RemoveEmptyEntries);

                string name = afterSplit[0];

                int len = afterSplit.Length - 1;

                double[] values = new double[len];

                for (int i = 0; i < len; i++)
                {
                    values[i] = double.Parse(afterSplit[i + 1], CultureInfo.InvariantCulture);
                }

                return new Operation(name, values);
            }
            else
            {
                throw new IncorrectParametersException();
            }
        }
    }
}

// реализуйте метод Parse(). 
// этод метод парсит строку inputString и возвращает объект Operation
// Формат строки: {имя_операции} {параметр1} ... {параметрN}
// Обратите внимание: 
// предварительно повторяющиеся пробелы и пробелы в начале и в конце нужно игнорировать
//
// Если что-то пойдет не так (например, из строки нельзя выделить 
// знак операции и как минимум один параметр), не забудьте сгенерировать 
// соответствующее исключение из папки Exceptions 
//
// Обратите внимание на юнит-тесты для этого класса