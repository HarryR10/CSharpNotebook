using System;
using System.Text;

namespace test
{
    public static class RandomMechanics
    {
        private static readonly Random _rnd = new Random();

        public static DateTime RandomDay()
        {
            DateTime start = new DateTime(2000, 1, 1);

            int range = (DateTime.Today - start).Days;

            return start.AddDays(_rnd.Next(range));
        }

        public static string RandomString()
        {
            const string sourceStr = "qwertyuiopasdfghjklzxcvbnm";

            char[] letters = sourceStr.ToCharArray();

            var resultString = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                resultString.Append(letters[_rnd.Next(letters.Length)].ToString());
            }
            return resultString.ToString();
        }

        public static int RandomNumber(int range)
        {
            return _rnd.Next(range);
        }

        public static int RandomNumber(int f, int s)
        {
            return _rnd.Next(f, s);
        }


        public static bool RandomBool()
        {
            if (_rnd.Next(2) == 1)
            {
                return true;
            }

            return false;
        }
    }
}
