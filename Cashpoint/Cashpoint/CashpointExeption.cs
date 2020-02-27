namespace CashpointModel
{
    using System;

    public class CellIsFullExeption : Exception
    {

        const string txtMessage = "Cell of this banknote is full";

        public CellIsFullExeption(string auxMessage) :
            base(String.Format("{0}: {1}", txtMessage, auxMessage))
        { }
    }

    public class BadNominalExeption : Exception
    {

        const string txtMessage = "Nominal was not added";

        public BadNominalExeption(string auxMessage) :
            base(String.Format("{0}: {1}", txtMessage, auxMessage))
        { }
    }
}