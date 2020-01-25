using System;

namespace Polynoms
{
    public class QuadEquation
    {
        //Получает в качестве аргументов коэффициенты уравнения
        //и значение x; после чего вычисляет сумму всех членов.
        public static double GetSummPolynom(double[] coefficients, double xValue)
        {
            Array.Reverse(coefficients);
            double result = 0;
            for(int cf = 0; cf < coefficients.Length; cf++)
            {
                result = result + coefficients[cf] * Math.Pow(xValue, cf);
            }
            return result;
        }

        //Решение квадратного уравнения
        public static double[] GetRoots(double[] coefficients)
        {
            double a = coefficients[0];
            double b = coefficients[1];
            double c = coefficients[2];

            double sensibility = 0.000000000001;

            if (Math.Abs(a) < sensibility)
            {
                var bd = (-c) / b;
                double[] result = { Math.Round(bd, 14) };
                return result;
            }

            double discriminant = Discriminant(a, b, c);
            if (Math.Abs(discriminant) < sensibility)
            {
                discriminant = 0;
                double[] result = { GetTwoArgs(a, b, discriminant)[0] };
                return result;
            }
            else if (discriminant < 0)
            {
                return null; // No roots
            }
            else
            {
                discriminant = Math.Sqrt(discriminant);
                double[] x = GetTwoArgs(a, b, discriminant);

                Math.Round(x[0], 14);
                Math.Round(x[1], 14);
                return x;
            }
        }

        private static double Discriminant(double a, double b, double c)
        {
            return Math.Pow(b, 2) - 4 * a * c;
        }

        private static double[] GetTwoArgs(double a, double b, double discriminant)
        {
            double xFirst = (-b + discriminant) / (2 * a);
            double xSecond = (-b - discriminant) / (2 * a);
            double[] result = { xFirst, xSecond };
            return result;
        }

    }
}
