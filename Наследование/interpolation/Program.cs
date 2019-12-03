using System;

public class Program
{
    private const double SamplePoint = 1.75;    // константа - объект не изменяемый
                                                // "может быть инициализировано только при объявлении поля"
                                                // readonly - может быть определен и при объявлении поля, и в конструкторе
                                                // поле readonly можно использовать для констант во время выполнения
                                                //
                                                // public static readonly uint timeStamp = (uint)DateTime.Now.Ticks
                                                //
                                                //https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/readonly#ref-readonly-return-example

    static void Main()
    {
        double[] values = { 0, 2, 1, 4 };

        object[] interpolators =                // а вот про это нужно прочитать подробнее
        {
            new StepInterpolator(values),
            new LinearInterpolator(values)
        };

        Console.WriteLine("Calculating value at sample point: {0}", SamplePoint);

        foreach (var interpolator in interpolators)
        {
            if (interpolator is CommonInterpolator)                             // is -- проверка типа, принадлежности к родительскому классу
                                                                                // as -- это уже приведение к типу "родителя"
                                                                                // оператор typeof получает экземпляр System.Type указанного типа
                                                                                // оператор приведения () выполняет явное преобразование ( например (int)value )
                                                                                // https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/type-testing-and-cast

            {
                Console.WriteLine("Class {0}: Interpolated value is {1}",
                    interpolator.GetType().Name,
                    (interpolator as CommonInterpolator).CalculateValue(SamplePoint));
            }
        }
    }
}

// ???
// Что дает такое преобразование:
//
//      (interpolator as CommonInterpolator).CalculateValue(SamplePoint))?
//      Это связано с тем, что изначально массив был типизирован как object[]?
//      и как отладчик понимает, что имеет дело например со StepInterpolator,
//      если происходит приведение к типу родительского класса?
//
// error CS1061: "object" не содержит определения для "CalculateValue", и не удалось найти доступный метод расширения "CalculateValue",
// принимающий тип "object" в качестве первого аргумента (возможно, пропущена директива using или ссылка на сборку).


                                                                                // Все объекты-потомки в C# могут быть приведены к одному 
                                                                                // из родительских типов в иерархии неявно.
                                                                                // Обратное приведение должно быть выполнено явно,
                                                                                // например с применением оператора as.




//--------------------более лаконичный вариант, мы сразу типизируем массив интерполяторов------------------
//class Program
//{
//    private const double SamplePoint = 1.75;

//    static void Main()
//    {
//        double[] values = { 0, 2, 1, 4 };

//        CommonInterpolator[] interpolators =
//        {
//            new StepInterpolator(values),
//            new LinearInterpolator(values)
//        };

//        Console.WriteLine("Calculating value at sample point: {0}", SamplePoint);

//        foreach (var interpolator in interpolators)
//        {
//            Console.WriteLine(
//                "Class {0}: Interpolated value is {1}",
//                interpolator.GetType().Name,
//                interpolator.CalculateValue(SamplePoint));
//        }
//    }
//}