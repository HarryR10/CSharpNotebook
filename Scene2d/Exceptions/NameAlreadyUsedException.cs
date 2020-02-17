namespace Scene2d.Exceptions
{
    using System;

    public class NameAlreadyUsedException : Exception
    {
        //когда в команде изменения используется несуществующее имя
        const string txtMessage = "Name already is used";

        public NameAlreadyUsedException(string auxMessage) :
            base(String.Format("{0}: {1}", txtMessage, auxMessage))
        { }
    }
}