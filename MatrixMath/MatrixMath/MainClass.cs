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

        // methods
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

        public Matrix Copy()
        {
            double[] allNumbers = new double[this.Columns * this.Rows];

            int index = 0;
            foreach (var row in this.GetRows)
            {
                foreach(var num in row)
                {
                    allNumbers.SetValue(num, index);
                    index++;
                }
            }
            return new Matrix(this.Columns, allNumbers);
        }

        public double[] ToArray()
        {
            int counter = 0;
            double[] result = new double[Rows * Columns];
            foreach(var row in this.GetRows)
            {
                foreach(double el in row)
                {
                    result[counter] = el;
                    counter++;
                }
            }
            return result;
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

            Matrix result = new Matrix(b.Columns, new double[b.Columns * a.Rows]);

            double[] mtrxArray = new double[a.Columns * a.Rows];


            int rowIterator = 0;
            foreach (var row in a.GetRows)
            {
                
                Matrix subResult = b.Copy();
                int index = 0;

                //создаем "подматрицу" - результат умножения каждого числа строки первой матрицы
                //на каждое число каждого столбца второй матрицы
                foreach(double num in row)
                {
                    //foreach(double subRow in subResult.GetRows[index])
                    var currentSubRow = subResult.GetRows[index];
                    for (int subIndx = 0; subIndx < subResult.Columns; subIndx++)
                    {
                        currentSubRow[subIndx] = currentSubRow[subIndx] * num;
                    }
                    index++;
                }

                //складываем числа по столбцам
                //сразу в матрицу-результат
                foreach (var summRow in subResult.GetRows)
                {
                    for (int el = 0; el < summRow.Length; el++)
                    {
                        result.GetRows[rowIterator][el] += summRow[el];
                    }
                }

                rowIterator++;
            }


            return result;
        }

        // additional
    }
}
