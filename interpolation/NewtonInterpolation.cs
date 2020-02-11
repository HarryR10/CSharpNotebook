using System;

internal class NewtonInterpolator : CommonInterpolator
{
    //разработана исходя из описания следующего алгоритма:
    //http://kontromat.ru/?page_id=4955

    public NewtonInterpolator(double[] values) : base(values)
    {
    }

    public override double CalculateValue(double x)
    {
        // если длина переданного массива нечетная
        // то добавляем последним элементом нулевое значение
        // (если все нечетные это x, а все четные - это y)
        bool isEven = false;

        int half = Values.Length / 2;
        if(half * 2 == Values.Length)
        {
            isEven = true;
        }
        else
        {
            half += 1;
        }

        // разбиваем на 2 массива
        double[] xArray = new double[half];
        double[] yArray = new double[half];

        GetXY(Values, xArray, yArray);

        // дополняем yArray, в случае необходимости
        if (!isEven)
        {
            yArray[half - 1] = xArray[half - 1]; //здесь можно добавить любое значение функции
        }

        // создаем двумерный массив для хранения данных значений y и f
        // т.е. разделенных разностей
        double[][] arrayFuncValue = new double[half][];

        // сразу вставляем значения y
        arrayFuncValue[0] = yArray;

        int intetnalLenght = half - 1;
        for (int i = 1; i < half; i++)
        {
            arrayFuncValue[i] = new double[intetnalLenght];
            intetnalLenght--;
        }

        // Определяем счетчик для навигации по arrayFuncValue и xArray
        // f[0] - указатель на массив со значениями y или f в arrayFuncValue. По этому указателю элементы массива будут заполняться
        // (по умолчанию = 1, т.к. y-значения мы уже вписали
        //
        // f[1], f[2] - указатели на элементы x в массиве xArray. По этим индексам мы будем проводить действия над значениями x
        // по окончании заполнения каждого последующего элемента с индексом f[0] счетчики x сбрасываются в значения
        // f[1] = f[0] + 1; - т.к. заходим на следующую итерацию
        // f[2] = 0;
        //
        // f[3], f[4] - указатели на элементы y в элементе массива arrayFuncValue, что указан как f[0] - 1. В дальнейшем мы используем foreach
        // и обращение к элементам происходит в форме el[f[3]] (т.е. мы перебираем элементы, которые являются источниками данных, а записываем уже в следующий
        // элемент, который имеет индекс f[0])
        // по окончании заполнения каждого последующего элемента с индексом f[0] счетчики x сбрасываются в значения
        // f[3] = 1;
        // f[4] = 0;
        int[] f = { 1, 1, 0, 1, 0 };

        foreach(var el in arrayFuncValue)
        {
            if(el.Length == 1)
            {
                break;
            }

            for(int i = 0; i < arrayFuncValue[f[0]].Length; i++)
            {

                if(Math.Abs((xArray[f[1]] - xArray[f[2]])) < Eps)
                {
                    string errorMsg = "Деление на ноль или значение, близкое к нулю. Возможно, значения x в интерполируемом массиве повторяются";
                    throw new Interpolation.DivideException(String.Format("Обнаружена ошибка: \"{0}\"", errorMsg));
                }

                arrayFuncValue[f[0]][i] = (el[f[3]] - el[f[4]]) / (xArray[f[1]] - xArray[f[2]]);
                f[1]++;
                f[2]++;
                f[3]++;
                f[4]++;
            }
            f[0]++;
            f[1] = f[0];
            f[2] = 0;
            f[3] = 1;
            f[4] = 0;
        }

        // перезаполняем массив xArray, чтобы не создавать новый массив
        for (int i = 0; i < xArray.Length; i++)
        {
            xArray[i] = x - xArray[i];
        }

        for (int i = 1; i < xArray.Length; i++)
        {
            xArray[i] = xArray[i] * xArray[i - 1];
        }

        
        double[] fArray = new double[arrayFuncValue.Length];
        int counter = 0;
        foreach(var el in arrayFuncValue)
        {
            fArray[counter] = el[0];
            counter++;
        }

        counter = 0;
        for (int i = 1; i < fArray.Length; i++)
        {
            fArray[i] = fArray[i] * xArray[counter];
            counter++;
        }

        double result = 0;
        foreach(var el in fArray)
        {
            result += el;
        }

        return result;
    }
}

