using System;
namespace Interpolation
{
    public class DivideException : Exception
    {
        const string txtMessage = "Ошибка умножения!";

        public DivideException(string auxMessage) :
            base(String.Format("{0} - {1}", txtMessage, auxMessage))
        {
            this.HelpLink = "https://docs.microsoft.com/ru-ru/dotnet/api/system.exception.stacktrace?view=netcore-2.1";
            this.Source = "Exception_Class_Samples";
        }
    }
}
