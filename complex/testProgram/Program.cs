using System;
using Vector;
using Complex;


namespace testProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            //------------------------------ВЕКТОРЫ-----------------------------
            {
                // создаем переменную - вектор.
                // можем использовать два конструктора:
                // 1)
                var ItsVector = new Vector2D(new double[] { 1, 1, 4, 14 });

                // 2)
                var ItsVector2 = new Vector2D(6, 8);

                // во втором случае подразумевается, начало вектора находится в точке (0,0)
                // в дальнейшем, мы унаследуем этот конструктор для класса комплексных чисел

                // после создания объектов мы можем обратиться к их свойствам:
                var CoordinatesArr = ItsVector.CoordinatesVector;
                var CoordinatesFullArr = ItsVector.Coordinates;

                // в обоих случаях мы получаем доступ к массиву с элементами типа double
                // различается лишь размерность
                // мы можем изменить координаты вектора, определив новый массив для поля Coordinates

                double[] NewPoints = { 2, 0, 4.9, 14 };
                ItsVector.Coordinates = NewPoints;


                // CoordinatesVector - это координаты вектора, они пересчитываются при обновлении поля (x, y)
                Console.WriteLine("Координаты вектора равны: " + ItsVector.CoordinatesVector[0] + "; " + ItsVector.CoordinatesVector[1]);

                // Coordinates - координаты двух точек на плоскости [x1, y1, x2, y2]
                Console.WriteLine("Координаты точки начала вектора равны: " + ItsVector.CoordinatesVector[0] + "; " + ItsVector.CoordinatesVector[1]);


                // для проверки на равенство 0 используется константа eps и ф-я checkEps
                // которая возвращает заданное в аргументе число, если его модуль больше eps
                // в противном случае возвращается 0

                // для векторов определены следующие методы

                // ToString:
                Console.WriteLine("ItsVector.ToString() = " + ItsVector.ToString());
                Console.WriteLine("ItsVector2.ToString() = " + ItsVector2.ToString());

                // Module - необходим для вычислений, в т.ч. комплексных чисел
                Console.WriteLine("Модуль вектора ItsVector = " + ItsVector.Module());

                // Multiply - умножение на число типа double
                Console.WriteLine("ItsVector * 2 =" + ItsVector.Multiply(2).ToString());

                // Copy - создание копии вектора
                var AnotherVector = ItsVector.Copy().Multiply(3);
                Console.WriteLine("AnotherVector = " + AnotherVector.ToString());


                // вектора можно складывать и вычитать
                Console.WriteLine("AnotherVector - ItsVector = " + (AnotherVector - ItsVector).ToString());
                Console.WriteLine("AnotherVector + ItsVector = " + (AnotherVector - ItsVector).ToString());
                // в данном случае результатом станет вектор с началом в точке (0, 0)
            }
            Console.WriteLine();
            //-------------------------КОМПЛЕКСНЫЕ ЧИСЛА------------------------
            {
                // комплексные числа перенимают один из конструкторов
                var ComplxNM = new complexNum(6, 8);

                // поля наследуются, + добавляется новое поле _isComplexNum (булево)
                Console.WriteLine("Координаты вектора равны: " + ComplxNM.CoordinatesVector[0] + "; " + ComplxNM.CoordinatesVector[1]);

                // из методов наследуется только Module()
                // Copy(), ToString() - свои
                // Multiply() - можно сделать public и переписать как new
                // (т.к. в родительском классе метод возвращает обьект Vector2D)
                var ComplxNM2 = new complexNum(1, 2);
                var ComplxNM3 = ComplxNM2.Copy();

                // операторы переопределены:
                ComplxNM = (ComplxNM2 + ComplxNM3) - ComplxNM;
                Console.WriteLine("Результат выражения (ComplxNM2 + ComplxNM3) - ComplxNM = " + ComplxNM.ToString());

                ComplxNM = (ComplxNM2 * ComplxNM3) / ComplxNM;
                Console.WriteLine("Результат выражения (ComplxNM2 * ComplxNM3) / ComplxNM = " + ComplxNM.ToString());

                // также появился метод, определяющий сопряженное значение комплексного числа (Сonjugate()):
                ComplxNM = ComplxNM.Сonjugate();
                Console.WriteLine("Число сопряженное ComplxNM = " + ComplxNM.ToString());

                // возведем комплексное число в степень:
                ComplxNM3 = complexNum.CXPow(ComplxNM2, 2);
                Console.WriteLine("Число ComplxNM2 возведенное в квадрат: " + ComplxNM3.ToString());

                // вычислим из него же квадратный корень:
                Console.WriteLine("Квадратный корень числа ComplxNM3 (CXPow()): " + complexNum.CXPow(ComplxNM2, (double)1 /2).ToString());

                Console.WriteLine("Квадратный корень числа ComplxNM3 (GetRoots(, int)): " + complexNum.GetRoots(ComplxNM2, 2)[0].ToString() + " ; " +
                                                                                            complexNum.GetRoots(ComplxNM2, 2)[1].ToString());

                Console.WriteLine("Квадратный корень числа ComplxNM3 (GetRoots(, double)): " + complexNum.GetRoots(ComplxNM2,       2.0)[0].ToString() + " ; " +
                                                                                               complexNum.GetRoots(ComplxNM2, (double)2)[1].ToString());

            }

            //double ll = (double)1 / 2;             // если взять 1/2 как ниже результатом будет 0
            //                                       // т.к. неявное преобразование всей правой части
            //                                       // после целочисленного деления int-ов
        }
    }
}
