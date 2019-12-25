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
        }

        static A DoStuff(A a)
        {
            a.value = 10;
            a = new A(15);
            return a;
        }
    }
