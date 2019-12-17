using System;
namespace Vector
{
    public class Vector2D
    {
        // Поля
        protected const double eps = 0.00000001;                // используется для проверки на равенство 0 (ф-я checkEps)
        protected double[] _coordinates = new double[4];
        protected double[] _coordinatesVector = new double[2];

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

            _coordinatesVector[0] = checkEps(X);
            _coordinatesVector[1] = checkEps(Y);
        }

        // get's & set's
        public double[] Coordinates
        {
            //get => _coordinates;
            get { return _coordinates; }
            set { ArrToCoordinates(value); }
        }

        public double[] CoordinatesVector
        {
            get => _coordinatesVector;
        }

        // functions
        protected void ArrToCoordinates(double[] CoordinatesArr)
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

            calcCoordinatesVector();
        }

        protected static double checkEps(double i)
        {
            if(Math.Abs(i) <= eps)
            {
                return 0;
            }
            return i;
        }

        protected void calcCoordinatesVector()
        {
            _coordinatesVector[0] = checkEps(_coordinates[2] - _coordinates[0]);
            _coordinatesVector[1] = checkEps(_coordinates[3] - _coordinates[1]);
        }

        // methods
        public double Module()
        {
            double isX = Math.Pow(_coordinatesVector[0], 2);
            double isY = Math.Pow(_coordinatesVector[1], 2);
            double Mod = checkEps(Math.Sqrt(isX + isY));
            return Math.Abs(Mod);
        }

        public override string ToString()
        {
            return "Vector <" + Math.Round(_coordinatesVector[0], 8) + "; "
                              + Math.Round(_coordinatesVector[1], 8) + ">";
        }

        public Vector2D Multiply(double Multiplier)
        {
            int counter = 0;
            foreach(var el in _coordinates)
            {
                _coordinates[counter] = el * Multiplier;
                counter++;
            }
            counter = 0;
            foreach (var el in _coordinatesVector)
            {
                _coordinatesVector[counter] = el * Multiplier;
                counter++;
            }
            return this;
        }

        public Vector2D Copy()
        {
            return new Vector2D(_coordinates);
        }

        // operators
        // операторы не переопределяются/не доступны в дочерних классах? virtual проставить здесь нельзя
        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            {
                // так не работает
                //double[] ResultCoordinates = new double[4];
                //for(int el = 0; el != 3; el++)
                //{
                //    ResultCoordinates[el] = checkEps(a.Coordinates[el] + b.Coordinates[el]);
                //}
                //return new Vector2D(ResultCoordinates);
            }
            return new Vector2D(checkEps(a.CoordinatesVector[0] + b.CoordinatesVector[0]),
                                checkEps(a.CoordinatesVector[1] + b.CoordinatesVector[1]));
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(checkEps(a.CoordinatesVector[0] - b.CoordinatesVector[0]),
                                checkEps(a.CoordinatesVector[1] - b.CoordinatesVector[1]));
        }


        // place for other methods
        //
        //...
    }
}
