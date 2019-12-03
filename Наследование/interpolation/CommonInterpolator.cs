internal class CommonInterpolator    // класс будет виден в любом месте содержащей его сборки
                                     // "поскольку класс Program находится с ним в одном пространстве имен - нас это устраивает"
{
    private double[] _values;

    public CommonInterpolator(double[] values)
    {
        _values = values;
    }

    public virtual double CalculateValue(double x)    // virtual - даёт нам возможность переписывать метод в дальнейшем
                                                      // это заглушка - метод может быть переопределен любым наследующим его классом:
                                                      // https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/virtual
                                                      //
                                                      // в дальнейшем мы столкнемся с такой ситуацией:
                                                      // метод в ChildClass скрывает одноименный метод в BaseClass
                                                      // https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/classes-and-structs/knowing-when-to-use-override-and-new-keywords
                                                      // Ключевое слово new сохраняет связи и подавляет предупреждения компилятора
                                                      // public new void ChildMethod()
                                                      // https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/new-modifier
                                                      //
                                                      // используя при объявлении экземпляра дочернего класса тип родителя мы, таким образом, имеем возможность использовать
                                                      // методы родительского класса
                                                      // а вот override метод, определенный в дочернем классе, можно вызывать при определении типа экземпляра как типа,
                                                      // "стояшего выше в иерархии" (апкаст)
    {
        return 0;
    }

    protected double[] Values
    {
        get { return _values; }
    }
}