using System;

internal class StepInterpolator : CommonInterpolator
{
    public StepInterpolator(double[] values) : base(values)    // в данном случае, происходит переопределение конструктора
                                                               // но можно вызвать и переопределенный метод из базового класса
                                                               // base.GetInfo(); -- например так
                                                               // https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/base


                                                               // base и this -- это ключевые слова доступа
                                                               // это указание на экземпляр класса, в котором происходят к-л "вычисления"
                                                               // У статических функций-членов нет указателя this, так как они существуют
                                                               // только на уровне класса и не являются частями объектов
                                                               // примеры кода тут:
                                                               // https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/this
    {
    }

    public override double CalculateValue(double x)            // ... CalculateValue
    {
        if (Values.Length > 0)
        {
            return Values[GetSafeIndex((int)Math.Round(x))];
        }

        return base.CalculateValue(x);                         // в данном случае мы проверяем условие и, если оно не выполняется,
                                                               // вызываем из подчиненного экземпляра класса (и даже из override метода!)
                                                               // одноименный метод из класса-родителя

    }

    private int GetSafeIndex(int index)
    {
        if (index < 0)
        {
            return 0;
        }

        if (index > Values.Length - 1)
        {
            return Values.Length - 1;
        }

        return index;
    }
}
