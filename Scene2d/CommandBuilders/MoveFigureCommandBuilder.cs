using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;

    public class MoveFigureCommandBuilder : ICommandBuilder
    {

        private static readonly Regex RecognizeRegex = new Regex(@"((\w*\-*)+)\s+\(\-?(\d+|\d+\.\d+|\d+\,\d+)\,\s?\-?(\d+|\d+\.\d+|\d+\,\d+)\)");

        private string _name;

        private bool _isScene = false;

        private ScenePoint _vector;

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

                _vector = new ScenePoint(double.Parse(afterSplit[1], CultureInfo.InvariantCulture), double.Parse(afterSplit[2], CultureInfo.InvariantCulture));
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
                return new MoveSceneCommand(_vector);
            }
            return new MoveCommand(_name, _vector);
        }   
    }
}