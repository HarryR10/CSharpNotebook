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

        private static readonly Regex RecognizeRegex = new Regex(@"\s+((\w*\-*)+)");

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

                if (afterSplit[0] == "scene")
                {
                    _isScene = true;
                }
                else
                {
                    _name = afterSplit[0];
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