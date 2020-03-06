using System;
using System.Text;

namespace JsonGenerator
{
    public class RandomMechanics
    { 
        public static DateTime RandomDay()
        {
            Random rnd = new Random();

            DateTime start = new DateTime(2000, 1, 1);

            int range = (DateTime.Today - start).Days;

            return start.AddDays(rnd.Next(range));
        }

        public static string RandomString()
        {
            Random rnd = new Random();

            const string sourceStr = "qwertyuiopasdfghjklzxcvbnm";

            char[] letters = sourceStr.ToCharArray();

            var resultString = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                resultString.Append(letters[rnd.Next(letters.Length)].ToString());
            }
            return resultString.ToString();
        }

        public static int RandomNumber(int range)
        {
            Random rnd = new Random();

            return rnd.Next(range);
        }

        public static int RandomNumber(int f, int s)
        {
            Random rnd = new Random();

            return rnd.Next(f, s);
        }


        public static bool RandomBool()
        {
            Random rnd = new Random();

            if (rnd.Next(2) == 1)
            {
                return true;
            }

            return false;
        }
    }
}
