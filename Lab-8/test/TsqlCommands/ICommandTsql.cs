using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.TsqlCommands
{
    interface ICommandTsql
    {
        void ReadCommand(string command); // здесь будет regex
        void ExecuteCommand(string connectionString);
    }
}
