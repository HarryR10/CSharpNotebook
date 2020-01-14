using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixMath
{
    public class Matrix
    {
        // fields
        private List<double[]> _matrix = new List<double[]>();

        public int Columns { get; }
        public int Rows { get; }
        public List<double[]> GetRows
        {
            get { return _matrix; }
        }

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
            Columns = columns;
            Rows = _matrix.Count;
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

        // operators
        public static Matrix operator *(Matrix a, Matrix b)
        {
            // check this
            if (a.Columns != b.Rows)
            {
                string errorMsg = "Количество столбцов не совпадает с количеством строк!";
                throw new MultiplyException(String.Format("Обнаружена ошибка: \"{0}\"", errorMsg));
            }

            Matrix result = new Matrix(a.Columns, new double[a.Columns * a.Rows]);

            int indexdRows = 0;
            foreach (var row in result.GetRows)
            {
                for(int id = 0; id < row.Length; id++)
                {
                    // error!
                    row[id] = a.GetRows[indexdRows][id] * b.GetRows[id][indexdRows];
                }
                indexdRows++;
            }

            return result;
        }

        // additional
    }
}
