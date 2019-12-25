static void Main(string[] args)
        {
            try
            {
                throw new ArgumentNullException();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("A");
            }
            catch (Exception)
            {
                Console.WriteLine("B");
                throw new Exception();
            }
            finally
            {
                Console.WriteLine("C");
            }
            Console.WriteLine("D");
        }
        
// A
// C
// D
// 91.6%
// Исключение поймает первый же подходящий под тип исключения блок catch (и выведет "A").
// Следующий блок catch игнорируется, поскольку исключение уже обработано.
// Исполнение переходит к блоку finally, в результате чего выводится "C".
// И наконец, поток исполнения продолжает работу за пределами try-catch, поскольку исключение было обработано, и выводит "D".
