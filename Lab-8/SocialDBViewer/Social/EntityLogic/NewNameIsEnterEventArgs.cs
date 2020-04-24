using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDb
{
    public class NewNameIsEnterEventArgs : EventArgs
    {
        public NewNameIsEnterEventArgs(string name)
        {
            Data = name;
            IsCommand = false;
        }

        public NewNameIsEnterEventArgs(string Command, bool isCommand)
        {
            Data = Command;
            IsCommand = isCommand;
        }
        public string Data { get; }

        public bool IsCommand { get; }
    }
}
