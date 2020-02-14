namespace Scene2d.Exceptions
{
    using System;

    public class UnexpectedEndOfPolygonExeption : Exception
    {
        const string txtMessage = "unexpected end of polygon";

        public UnexpectedEndOfPolygonExeption(string auxMessage) : base(String.Format("{0}: {1}", txtMessage, auxMessage)) { }
    }
}