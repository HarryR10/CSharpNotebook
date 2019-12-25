private delegate void SomeMethod();
        static void Main()
        {
            var list = new List<SomeMethod>();
            for (var i = 0; i < 3; i++)
            {
                list.Add(() => Console.WriteLine(i));
            }

            foreach (var del in list)
            {
                del();
            }
        }
        
