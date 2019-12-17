using System;
using Vector;
namespace Complex
{
    public class complexNum : Vector2D
    {
        // Поля
        readonly bool _isComplexNum = new bool();

        // Конструкторы
        public complexNum(double X, double Y) : base(X , Y)
        {
            _isComplexNum = true;
        }

        // get's & set's
        // functions
        public static complexNum CXPow(complexNum Num, double n)
        {
            double Angle = n * Math.Atan(Num.CoordinatesVector[1] / Num.CoordinatesVector[0]);
            double Module = Num.Module();
            double Natural = (Math.Cos(Angle) ) * Math.Pow(Module, n);     // * (180 / Math.PI)
            double Imaginary = (Math.Sin(Angle) ) * Math.Pow(Module, n);   // не в этом случае
            return new complexNum(Natural, Imaginary);
        }

        public static complexNum[] GetRoots(complexNum Num, int n)
        {
            complexNum[] Result = new complexNum[n];
            double nd = (double)n;
            double Angle = new double();
            double Module = Num.Module();

            for (int k = 0; k != n; k++)
            {
                Angle = (Math.Atan(Num.CoordinatesVector[1] / Num.CoordinatesVector[0]) + 2 * Math.PI * k) / nd;
                // Angle = Math.Atan(Num.CoordinatesVector[1] / Num.CoordinatesVector[0]) / n;
                double Natural = Math.Pow(Module, 1 / nd) * Math.Cos(Angle);
                double Imaginary = Math.Pow(Module, 1 / nd) * Math.Sin(Angle);
                Result[k] = new complexNum(Natural, Imaginary);
            }
            return Result;            
        }

        public static complexNum[] GetRoots(complexNum Num, double n)
        {
            // почти тот же CXPow...
            complexNum[] Result = new complexNum[(int)n];
            double Angle = new double();
            double Module = Num.Module();
            double nR = (double)1 / n;    // на всякий случай, объявляем явно, что 1 это не int
            for (int k = 0; k != (int)n; k++)
            {
                Angle = nR * (Math.Atan(Num.CoordinatesVector[1] / Num.CoordinatesVector[0]) + 2 * Math.PI * k);
                double Natural = (Math.Cos(Angle)) * Math.Pow(Module, nR);     
                double Imaginary = (Math.Sin(Angle)) * Math.Pow(Module, nR);   
                Result[k] = new complexNum(Natural, Imaginary);
            }
            return Result;
        }
        // methods
        public override string ToString()
        {
            string Imginary = "";
            if(_coordinatesVector[1] > 0)
            {
                Imginary = " + " + Math.Round(_coordinatesVector[1], 8) + "i";
            }
            else if(_coordinatesVector[1] < 0)
            {
                Imginary = " - " + Math.Abs(Math.Round(_coordinatesVector[1], 8)) + "i";
            }
            else if(checkEps(_coordinatesVector[1]) == 0)
            {
                return Math.Round(_coordinatesVector[0], 8).ToString();
            }
            return Math.Round(_coordinatesVector[0], 8) + Imginary;
        }

        public string ToTrgmtryStr()
        {
            return "";
        }

        public new complexNum Copy()
        {
            return new complexNum(_coordinatesVector[0], _coordinatesVector[1]);
        }

        public complexNum Сonjugate()
        {
            double Imaginary = _coordinatesVector[1];
            if (Math.Sign(Imaginary) == 1 || Math.Sign(Imaginary) == 0)
            {
                return new complexNum(_coordinatesVector[0], Imaginary);
            }
            return new complexNum(_coordinatesVector[0], -1 * Imaginary);
        }

        // operators
        public static complexNum operator +(complexNum a, complexNum b)
        {
            return new complexNum(checkEps( a.CoordinatesVector[0] + b.CoordinatesVector[0]),
                                  checkEps( a.CoordinatesVector[1] + b.CoordinatesVector[1]));
        }

        public static complexNum operator -(complexNum a, complexNum b)
        {
            return new complexNum(checkEps(a.CoordinatesVector[0] - b.CoordinatesVector[0]),
                                  checkEps(a.CoordinatesVector[1] - b.CoordinatesVector[1]));
        }

        public static complexNum operator *(complexNum a, complexNum b)
        {
            double Natural = checkEps(a.CoordinatesVector[0] * b.CoordinatesVector[0] - a.CoordinatesVector[1] * b.CoordinatesVector[1]);
            double Imaginary = checkEps(a.CoordinatesVector[1] * b.CoordinatesVector[0] + a.CoordinatesVector[0] * b.CoordinatesVector[1]);
            return new complexNum(Natural, Imaginary);
        }

        public static complexNum operator /(complexNum a, complexNum b)
        {
            double Denominator = Math.Pow(b.CoordinatesVector[0], 2) + Math.Pow(b.CoordinatesVector[1], 2);
            if(checkEps(Denominator) == 0)
            {
                // здесь можно подключить логгирование или вызвать исключение
                return new complexNum(0, 0);
            }
            double Natural = a.CoordinatesVector[0] * b.CoordinatesVector[0] + a.CoordinatesVector[1] * b.CoordinatesVector[1];
            double Imaginary = checkEps(a.CoordinatesVector[1] * b.CoordinatesVector[0] - a.CoordinatesVector[0] * b.CoordinatesVector[1]);
            return new complexNum(Natural / Denominator,
                                  Imaginary / Denominator);
        }
    }
}
