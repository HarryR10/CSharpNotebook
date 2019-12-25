 class A
    {
        public int value;
        public A(int value)
        {
            this.value = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a = new A(5);
            DoStuff(a);
            Console.WriteLine(a.value);
            
            // var a = new A(5);
            // a = DoStuff(a);
            // Console.WriteLine(a.value);
            // =15
        }

        static A DoStuff(A a)
        {
            a.value = 10;
            a = new A(15);
            return a;
        }
    }
// Вопрос - что выведет программа

// результатом будет 10, т.к. DoStuff(A a) хоть и возвращает значение, но оно не присвоено в коде Main
// одновременно с этим идет присвоение свойства value для уже объявленного экземпляра
