using System;
namespace Vector
{
    public class Vector2D
    {
        // Поля
        private const double eps = 0.00000001;
        private double[] _coordinates = new double[4];

        // Конструкторы
        public Vector2D(double[] CoordinatesArr)
        {
            {
                // в данном случае произойдет остановка работы программы
                //if(Coordinates.Length>4 || Coordinates.Length < 1)
                //{
                //    throw new IndexOutOfRangeException();
                //}
            }

            // Заполняем поле Coordinates
            // массив запишет координаты в любом случае

            ArrToCoordinates(CoordinatesArr);

        }

        public Vector2D(double X, double Y)
        {
            _coordinates[0] = 0;
            _coordinates[1] = 0;
            _coordinates[2] = checkEps(X);
            _coordinates[3] = checkEps(Y);
        }

        // get's & set's
        public double[] Coordinates
        {
            //get => _coordinates;
            get { return _coordinates; }
            set { ArrToCoordinates(value); }
        }

        // functions
        protected internal void ArrToCoordinates(double[] CoordinatesArr)
        {
            for (int el = 0; el < 4; el++)
            {
                try
                {
                    _coordinates[el] = checkEps(CoordinatesArr[el]);
                }
                catch
                {
                    _coordinates[el] = 0;
                }
            }
        }

        protected internal double checkEps(double i)
        {
            if(Math.Abs(i) <= eps)
            {
                return 0;
            }
            return i;
        }

        // methods
        public static double Module()
        {
            double isX = _coordinates[2] - _coordinates[0];
            double isY = _coordinates[3] - _coordinates[1];
            double Mod = checkEps(Math.Sqrt(isX + isY));
            return Math.Abs(Mod);
        }
    }
}
