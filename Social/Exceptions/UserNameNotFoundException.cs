using System;
namespace Social.Exceptions
{
    public class UserNameNotFoundException : Exception
    {
        public UserNameNotFoundException(string message) : base(message)
        {
        }
    }
}
