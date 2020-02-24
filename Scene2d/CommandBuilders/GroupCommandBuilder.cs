using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;

    public class GroupCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(
            @"group\s+(\w+|\-+)+(\,\s+(\w+|\-+)+)*\s+as\s+(\w+|\-+)+");

        private List<string> _childFigures = new List<string>();

        private string _name;

        public bool IsCommandReady
        {
            get
            {
                return true;
            }
        }

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                string[] separators = { " ", "(", ",", ")" };
                string[] afterSplit = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                _name = afterSplit[afterSplit.Length - 1];
                for(int i = 1; i < afterSplit.Length - 2; i++)
                {
                    _childFigures.Add(afterSplit[i]);
                }

            }
            else
            {
                throw new BadFormatException(line);
            }
        }

        public ICommand GetCommand() => new GroupCommand(_name, _childFigures);
    }
}