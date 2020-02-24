using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Figures;

    public class DeleteFigureCommandBuilder : ICommandBuilder
    {
        //было без delete
        private static readonly Regex RecognizeRegex = new Regex(@"delete\s+((\w+|\-+)+)");

        private string _name;

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
            // check if line matches the RecognizeRegex
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                string[] separators = { " ", "(", ",", ")" };
                string[] afterSplit = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                // было 0
                if (afterSplit[1] == "scene")
                {
                    _isScene = true;
                }
                else
                {
                    _name = afterSplit[1];
                }
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
                return new DeleteSceneCommand();
            }
            return new DeleteCommand(_name);
        }
    }
}