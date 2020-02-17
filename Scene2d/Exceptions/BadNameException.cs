namespace Scene2d.Exceptions
{
    using System;

    public class BadNameException : Exception
    {
        //когда в команде изменения используется несуществующее имя
        const string txtMessage = "Figure or group is not found";

        public BadNameException(string auxMessage) :
            base(String.Format("{0}: {1}", txtMessage, auxMessage))
        { }
    }
}