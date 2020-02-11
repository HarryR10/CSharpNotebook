namespace Scene2d.Exceptions
{
    using System;

    public class BadFormatException : Exception
    {
        const string txtMessage = "bad format";

        public BadFormatException(string auxMessage) : base(String.Format("{0}: {1}", auxMessage, txtMessage)) { }
    }
}
