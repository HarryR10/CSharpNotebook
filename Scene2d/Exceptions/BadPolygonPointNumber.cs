namespace Scene2d.Exceptions
{
    using System;

    public class BadPolygonPointNumber : Exception
    {
        const string txtMessage = "need more points!";

        public BadPolygonPointNumber(string auxMessage) : base(String.Format("{0}: {1}", auxMessage, txtMessage)) { }
    }
}