using System;
using Vector;

namespace testProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            double[] mmm = { 1, 6, 89};
            var yyy = new Vector2D(mmm);
            var aa = Vector2D.Module();
            Console.WriteLine(aa);
        }
    }
}
