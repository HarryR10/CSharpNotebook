namespace Scene2d.Exceptions
{
    using System;

    public class BadRectanglePointException : Exception
    {
        //когда точки не задают прямоугольник
        
        public BadRectanglePointException(string auxMessage) :
            base(String.Format("points {0} isn't define a rectangle", auxMessage)) { }
    }
}