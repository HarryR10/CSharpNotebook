using SocialDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate ComplexEntityData SearchingName(NewNameIsEnterEventArgs e);
public delegate void ExecuteCommand(NewNameIsEnterEventArgs e);

namespace SocialDb
{
    public class UILogic
    {
        public event ExecuteCommand CommandIsAdded;

        public event SearchingName NameIsWrite;

        public void OnCommandIsAdded(NewNameIsEnterEventArgs e)
        {
            if (e.IsCommand)
            {
                ExecuteCommand temp = CommandIsAdded;
                temp?.Invoke(e);
            }
            else
            {
                SearchingName temp = NameIsWrite;
                if(temp != null)
                {
                    var result = temp.Invoke(e);
                    DisplayEntityData.DisplayOnConsole(result);
                }
            }
        }
    }
}
