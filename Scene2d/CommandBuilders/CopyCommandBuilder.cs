using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;

    public class CopyCommandBuilder : ICommandBuilder
    {

        private static readonly Regex RecognizeRegex = new Regex(
            @"copy\s+(\w+|\-+)+\s+to\s+(\w+|\-+)+");
        //(\w+|\-+)+
        private string _name;

        private string _copyName;

        private bool _isScene = false;

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

                if (afterSplit[1] == "scene")
                {
                    _isScene = true;
                }
                else
                {
                    _name = afterSplit[1];
                }

                _copyName = afterSplit[3];
            }
            else
            {
                throw new BadFormatException("error in line");
            }
        }

        public ICommand GetCommand()
        {
            if (_isScene)
            {
                return new CopySceneCommand(_copyName);
            }
            return new CopyCommand(_name, _copyName);
        }
    }
}