static void Main(string[] args)
        {
            var result = Square(null);
            Console.WriteLine(result);
        }

        static IEnumerable<int> Square(IEnumerable<int> items)
        {
            foreach (var item in items)
            {
                yield return item * item;
            }
        }
        
// ???
