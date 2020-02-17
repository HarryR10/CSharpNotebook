using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Figures;

    public class RotateComandBuilder : ICommandBuilder
    {

        private static readonly Regex RecognizeRegex = new Regex(@"rotate\s+((\w*\-*)+)\s+\-?(\d+|\d+\.\d+|\d+\,\d+)");

        private string _name;

        private double _angle;

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

                if (afterSplit[1] == "scene")
                {
                    _isScene = true;
                }
                else
                {
                    _name = afterSplit[1];
                }

                _angle = double.Parse(afterSplit[2], CultureInfo.InvariantCulture);
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
                return new RotateSceneCommand();
            }
            return new RotateCommand(_name, _angle);
        }
    }
}