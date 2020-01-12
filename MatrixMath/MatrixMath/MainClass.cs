using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixMath
{
    public class Matrix
    {
        // fields
        List<double[]> _matrix = new List<double[]>();

        // construct
        public Matrix(int columns, params double[] numbers)
        {
            int numLenght = numbers.Length;
            double[] line = new double[columns];

            for (int i = 0; i < numLenght; i++)
            {
                if (i % columns == 0 & i != 0)
                {
                    _matrix.Add(line);
                    line = new double[columns];
                }
                line[i % columns] = numbers[i];
            }
            _matrix.Add(line);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var i in _matrix)
            {
                result.Append("(");
                bool itsBegin = true;

                foreach (var el in i)
                {
                    if (itsBegin)
                    {
                        result.Append(el);
                        itsBegin = false;
                        continue;
                    }
                    result.Append(", " + el);
                }
                result.Append("); ");
            }
            result.Remove(result.Length - 2, 2);
            string strRes = result.ToString();
            return strRes;
        }

        // static
        public static Matrix QuadraticMx()
        {
            double[] numbers = { 1, 0, 0, -3, 4, -1, 2, -4, 2 };
            return new Matrix(3, numbers);
        }
    }
}
