using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDb.TsqlCommands
{
    interface ICommandTsql
    {
        //вероятно, лучше бы подошел абстрактный класс...
        void ReadCommand(string command); // здесь будет regex
        void ExecuteCommand(string connectionString);
    }
}
