namespace Scene2d.Exceptions
{
    using System;

    public class BadCircleRadiusException : Exception
    {
        //когда радиус окружности <= 0
        //отрицательный радиус проверяется в регулярном выражении при парсинге строки
        //в реализованной логике при отрицательном радиусе будет выдаваться ошибка
        //"Bad Format"
        public BadCircleRadiusException(string auxMessage) : base(String.Format("radius of circle {0} <= 0", auxMessage)) { }
    }
}