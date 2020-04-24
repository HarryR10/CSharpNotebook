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
    public static class SqlQueryLogic
    {
        public static void MakeAndExecuteSQLQuery(NewNameIsEnterEventArgs command)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

            Dictionary<Regex, Func<ICommandTsql>> CmdList = new Dictionary<Regex, Func<ICommandTsql>>
            {
                { new Regex(@"^add one user.+"), () => new AddOneUserCommand() },
                { new Regex(@"^add one offer.+"), () => new AddOneOfferCommand() },
            };


            string newline = command.Data;
            bool isBadCommand = true;

            foreach (var rgx in CmdList)
            {
                if (rgx.Key.IsMatch(newline))
                {
                    var tempCmd = rgx.Value.Invoke();
                    tempCmd.ReadCommand(newline);
                    tempCmd.ExecuteCommand(connectionString);
                    isBadCommand = false;
                }
            }

            if (isBadCommand)
            {
                throw new BadFormatException();
            } 
        }

        public static void MakeAndExecuteSQLQuery(NewNameIsEnterEventArgs command, string connectionString)
        {
            Dictionary<Regex, Func<ICommandTsql>> CmdList = new Dictionary<Regex, Func<ICommandTsql>>
            {
                { new Regex(@"^add one user.+"), () => new AddOneUserCommand() },
                { new Regex(@"^add one offer.+"), () => new AddOneOfferCommand() },
            };


            string newline = command.Data;
            bool isBadCommand = true;

            foreach (var rgx in CmdList)
            {
                if (rgx.Key.IsMatch(newline))
                {
                    var tempCmd = rgx.Value.Invoke();
                    tempCmd.ReadCommand(newline);
                    tempCmd.ExecuteCommand(connectionString);
                    isBadCommand = false;
                }
            }

            if (isBadCommand)
            {
                throw new BadFormatException();
            }
        }
    }
}
