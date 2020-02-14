namespace Scene2d.Exceptions
{
    using System;

    public class BadPolygonPointNumberException : Exception
    {
        const string txtMessage = "need more points!";

        public BadPolygonPointNumberException(string auxMessage) : base(String.Format("{0}: {1}", auxMessage, txtMessage)) { }
    }
}
