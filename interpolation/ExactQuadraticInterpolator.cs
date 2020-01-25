using System;
using System.Collections.Generic;
using MatrixMath;
using Polynoms;

internal class ExactQuadraticInterpolator : CommonInterpolator
{
    //разработана с учетом этого алгоритма:
    //https://www.youtube.com/watch?v=-dqR7IhU1t0


    public ExactQuadraticInterpolator(double[] values) : base(values)
    {
    }

    public override double CalculateValue(double x)
    {
        //для интерполяции по указанному алгоритму требуется указать координаты
        //как минимум для трех точек.
        //нулевые координаты добавляются в начало
        double[] currentValues = new double[6];

        if (Values.Length < 6)
        {
            int offset = 6 - Values.Length;

            for (int el = 0; el + offset < 6; el++)
            {
                currentValues[el + offset] = Values[el];
            }
        }
        else
        {
            for(int i = 0; i < currentValues.Length; i++)
            {
                currentValues[i] = Values[i];
            }
        }

        //преобразуем коордиаты в матрицы с одним столбцом
        //для x и y

        double[] xCoordinates = new double[currentValues.Length / 2];
        double[] yCoordinates = new double[currentValues.Length / 2];
        int[] miniCounter = { 0, 0 };

        for (int i = 0; i < currentValues.Length; i++)
        {
            int sort = i % 2;
            if(sort == 0)
            {
                xCoordinates[miniCounter[sort]] = currentValues[i];
            }
            else
            {
                yCoordinates[miniCounter[sort]] = currentValues[i];
            }
            miniCounter[sort]++;

        }
        var xColumn = new Matrix(1, xCoordinates);
        var yColumn = new Matrix(1, yCoordinates);

        //получаем коэффициенты параметрического уравнения
        var xCoefficients = (Matrix.QuadraticMx() * xColumn).ToArray();
        var yCoefficients = (Matrix.QuadraticMx() * yColumn).ToArray();

        Array.Reverse(xCoefficients);
        Array.Reverse(yCoefficients); //приводим к стандартной математической записи

        xCoefficients[2] = xCoefficients[2] - x;

        //необходимо решить квадратное уравнение с коэффициентами xCoefficients
        //и подставить результат в уравнение с yCoefficients
        double[] rootsFrstPolynom = QuadEquation.GetRoots(xCoefficients);

        List<double> resultInterpolation = new List<double>();
        foreach (double root in rootsFrstPolynom)
        {
            resultInterpolation.Add(QuadEquation.GetSummPolynom(yCoefficients, root));
        }

        //т.к. в базовом классе мы можем вернуть только одно значение double...
        //return resultInterpolation;
        return resultInterpolation[0];
    }
}