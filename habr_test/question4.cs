class Program
    {
        class A
        {
            public void Print()
            {
                Console.WriteLine("A");
            }
        }

        class B : A
        {
            new virtual public void Print()
            {
                Console.WriteLine("B");
            }
        }

        class C : B
        {
            override public void Print()
            {
                Console.WriteLine("C");
            }
        }

        static void Main(string[] args)
        {
            var x1 = new C(); 
            x1.Print();

            B x2 = new C(); 
            x2.Print();

            A x3 = new C(); 
            x3.Print();
        }
    }
    
    // варианты:
    // CCC, CCA, CBA, программа не соберется...
    // CCC - верный вариант
