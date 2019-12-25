public class NotAStaticClass
    {
        static NotAStaticClass()
        {
            throw new InvalidDataException("From static ctor");
        }
        public Task DoStuff()
        {
            Console.WriteLine("Hello World!");
            return Task.CompletedTask;
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (o, args) => Console.WriteLine("Exception handled globally: " + args.ExceptionObject.Ge tType().Name);
            try
            {
                await new NotAStaticClass().DoStuff();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception handled with catch block: " + e.GetType().Name);
            }
        }
    }

// Exception handled with catch block: TypeInitializationException
// 62.2%
// Если конструктор типа завершается с исключением, то это исключение оборачивается в TypeInitizationException, 
// которое выбрасывается при данном и последующих обращениях к типу.
