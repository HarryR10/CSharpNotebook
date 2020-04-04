using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.Exceptions;
using test.TsqlCommands;

namespace test
{
    static class SqlQueryLogic
    {
        public static void MakeAndExecuteSQLQuery()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

            Dictionary<Regex, Func<ICommandTsql>> CmdList = new Dictionary<Regex, Func<ICommandTsql>>
            {
                { new Regex(@"^add one user.+"), () => new AddOneUserCommand() },
            };


            string newline = Console.ReadLine();

            foreach (var rgx in CmdList)
            {
                if (rgx.Key.IsMatch(newline))
                {
                    var tempCmd = rgx.Value.Invoke();
                    tempCmd.ReadCommand(newline);
                    tempCmd.ExecuteCommand(connectionString);
                }
                else
                {
                    throw new BadFormatException();
                }
            }
            Console.ReadLine();
        }
    }
}
