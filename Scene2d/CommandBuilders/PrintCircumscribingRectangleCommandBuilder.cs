using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;

    public class PrintCircumscribingRectangleCommandBuilder : ICommandBuilder
    {

        private static readonly Regex RecognizeRegex = new Regex(
            @"print circumscribing rectangle for\s+((\w*\-*)+)");

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
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                string[] separators = { " ", "(", ",", ")" };
                string[] afterSplit = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                if (afterSplit[4] == "scene")
                {
                    _isScene = true;
                }
                else
                {
                    _name = afterSplit[4];
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
                // todo: make it
                //return new PrintCircumscribingRectangle();
            }
            return new PrintCircumscribingRectangle(_name);
        }
    }
}