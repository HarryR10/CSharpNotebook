namespace Calculator
{
    using System;

    using Exceptions;

    public static class Program
    {
        delegate void Message(string message);

        public static void Main()
        {
            ICalculatorEngine calculator = new CalculatorEngine();
            IParser parser = new Parser();

            //мы можем переопределить вывод данных здесь
            Message ToConsole = Console.WriteLine;

            try
            {
                // пример определяемых операций
                // (сейчас их добавление в калькулятор не реализовано - это ваша задача)

                var sqrt = new Func<double, double>(
                    // пример многострочной лямбды
                    x =>
                    {
                        if (x < 0)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        return Math.Sqrt(x);
                    });

                calculator.DefineOperation("sqrt", sqrt);

                // можно использовать одинаковое имя для операций с разным количеством аргументов
                calculator.DefineOperation("-", a => -a);
                calculator.DefineOperation("-", (a, b) => a - b);

                // обратите внимание: подставляется напрямую метод класса Math
                // это эквивалентно calculator.DefineOperation("^", (x, y) => Math.Pow(x, y)), но лаконичнее
                calculator.DefineOperation("^", Math.Pow);

                // ... определите остальные операции здесь ...
                calculator.DefineOperation("+", (a, b) => a + b);
                calculator.DefineOperation("+", (a, b, c) => a + b + c);

                calculator.DefineOperation("Abs", Math.Abs );

                calculator.DefineOperation("Root", (a, b) =>

                {
                    if (Math.Abs(b) > 0.000001)
                        return Math.Pow(a, 1 / b);
                    else
                        throw new ArgumentOutOfRangeException();
                });

                

            }
            catch (AlreadyExistsOperationException)
            {
                Console.WriteLine("This operation already exists in the calculator");
            }

            var evaluator = new Evaluator(calculator, parser);
            Console.WriteLine("Please enter expressions: ");

            while (true)
            {
                string line = Console.ReadLine();
                if (line == null || line.Trim().Length == 0)
                {
                    break;
                }

                try
                {
                    ToConsole(evaluator.Calculate(line));
                }
                catch (NotFoundOperationException)
                {
                    ToConsole("Operation in line " + line + " is not found");
                    // Выбрасываем это исключение когда в методе ICalculatorEngine.PerformOperation вызывается
                    // несуществующая операция.
                }
                catch (IncorrectParametersException)
                {
                    ToConsole(@"Method ""IParser.Parse"" not define arguments in line: " + line);
                    // Выбрасываем это исключение когда метод IParser.Parse не находит
                    // ни одного аргумента во входной строке.
                }
                catch (ParametersCountMismatchException)
                {
                    ToConsole("Operations with given number of parameters is not found!");
                    // Выбрасываем это исключение когда в методе ICalculatorEngine.PerformOperation вызывается
                    // операция с неверным числом параметров.
                }
                catch (ArgumentOutOfRangeException)
                {
                    ToConsole("One or some arguments is not match certain requirement!");
                }
            }
        }
    }
}
