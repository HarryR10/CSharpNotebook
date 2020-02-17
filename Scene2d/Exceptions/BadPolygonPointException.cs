namespace Scene2d.Exceptions
{
    using System;

    public class BadPolygonPointException : Exception
    {
        //когда точка полигона совпадает с одной из предыдущих или образует
        //пересечение с одной из предыдущих сторон
        const string txtMessage = "with this point a polygon will be not formed";

        public BadPolygonPointException(string auxMessage) :
            base(String.Format("{0}: {1}", txtMessage, auxMessage)) { }
    }
}