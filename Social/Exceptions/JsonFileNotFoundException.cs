using System;

namespace Social.Exceptions
{
    public class JsonFileNotFoundException : Exception
    {
        public JsonFileNotFoundException(string message) : base(message)
        {
        }
    }
}