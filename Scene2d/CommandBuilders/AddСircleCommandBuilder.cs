using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Figures;

    public class AddСircleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = new Regex(
            @"^add circle\s+(\w+|\-+)+\s+\(\-?((\d+\.\d+)|(\d+))\,\s?\-?((\d+\.\d+)|(\d+))\)\s+radius\s+((\d+\.\d+)|(\d+))");

        private IFigure _circle;

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

                    _name = afterSplit[2];
                    double radius = double.Parse(afterSplit[6], CultureInfo.InvariantCulture);
                    ScenePoint center = new ScenePoint(double.Parse(afterSplit[3], CultureInfo.InvariantCulture), double.Parse(afterSplit[4], CultureInfo.InvariantCulture));

                    if (Math.Abs(radius) < 0.0000000001 | radius < 0)
                    {
                        throw new BadCircleRadiusException(radius.ToString());
                    }
                    _circle = new CircleFigure(center, radius);
                
            }
            else
            {
                throw new BadFormatException(line);
            }
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _circle);
    }
}