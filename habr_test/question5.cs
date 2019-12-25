using System;
using System.Linq;

class Program
    {
        public static void Main(string[] args)
        {
            Do(true);
            Do(DateTimeKind.Local);
            Do("Hello");
            Do(new object());

            var doMethodsCount = typeof(Program).GetMethods().Count(m => m.Name.StartsWith("Do"));
            Console.WriteLine(doMethodsCount);
        }

        public static void Do<T>(T value)
        {
        }
    }
    
//1, а вот при JIT компиляции будет создано 3 метода.Один метод — для всех ссылочных типов(работа со ссылками всегда одинаковая, 
// поэтому и метод один), 
//плюс по одному методу для каждого value type(поскольку у value type разный размер в памяти).

//62.2%
//Верно! А пояснения уже содержатся в ответе.
