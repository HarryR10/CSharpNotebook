using System;
namespace Vector
{
    public class Vector2D
    {
        // Поля
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
            _coordinates[2] = X;
            _coordinates[3] = Y;
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
                    _coordinates[el] = CoordinatesArr[el];
                }
                catch
                {
                    _coordinates[el] = 0;
                }
            }
        }
    }
}
