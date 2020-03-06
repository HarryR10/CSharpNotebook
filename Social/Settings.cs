using System;

namespace Social.Models
{
    [Flags]
    public enum MessageRecipient
    {
        Console     =  001,
        Log         =  010,
        Email       =  100 // etc...

        //Console = 0b_0000_0000,
        //Log = 0b_0000_0001,
        //Email = 0b_0000_0010 // etc...
    }

    public enum ColorScheme
    {
        None,
        Warm,
        Cold // etc...
    }

    public struct Settings
    {
        public bool RelativePath { get; set; }

        public string PathDirectory { get; set; }

        public MessageRecipient Output { get; set; }

        public string EmailAdress { get; set; }

        public ColorScheme Scheme { get; set; }
    }
}
