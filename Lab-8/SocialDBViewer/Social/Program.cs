using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SocialDb.Exceptions;
using SocialDb.TsqlCommands;

namespace SocialDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DataModel db = new DataModel())
            {
                while (true)
                {
                    var crnt = new EntityDataExtractors(db);
                    var ui = new UILogic();

                    Console.WriteLine("Enter a name or command. Commands are available:");
                    Console.WriteLine("(add one user [name], [gender(male|female)], [birthday(dd.мм.yyyy)], [last visit date(dd.мм.yyyy)], [is online(y|n)])");
                    Console.WriteLine("(add one offer [name of sender], [name of receiver], [status this offer]");
                    var userText = Console.ReadLine();

                    NewNameIsEnterEventArgs e;
                    if (userText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                    {
                        e = new NewNameIsEnterEventArgs(userText, true);
                        ui.CommandIsAdded += SqlQueryLogic.MakeAndExecuteSQLQuery;
                    }
                    else if(userText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length == 1)
                    {
                        e = new NewNameIsEnterEventArgs(userText);
                        ui.NameIsWrite += crnt.GetComplexSqlData;
                    }
                    else
                    {
                        continue;
                    }
                    ui.OnCommandIsAdded(e);
                }
            }
        }
    }
}
