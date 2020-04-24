using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDb.TsqlCommands
{
    struct SqlDataSet
    {
        public Dictionary<string, string> Parameters;

        public string QueryText;
        // возможно, больше подойдет для сложных запросов
        // public StringBuilder QueryText;
    }
}
