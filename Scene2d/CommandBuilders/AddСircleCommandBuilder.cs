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
        private static readonly Regex RecognizeRegex = new Regex(@"(\w*\-*)+\s+\(\-?(\d+|\d+\.\d+|\d+\,\d+)\,\s?\-?(\d+|\d+\.\d+|\d+\,\d+)\)\s+radius\s+(\d+|\d+\.\d+|\d+\,\d+)");

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
                //try
                //{
                    string[] separators = { " ", "(", ",", ")" };
                    string[] afterSplit = match.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    _name = afterSplit[0];
                    double radius = double.Parse(afterSplit[4], CultureInfo.InvariantCulture);
                    ScenePoint center = new ScenePoint(double.Parse(afterSplit[1], CultureInfo.InvariantCulture), double.Parse(afterSplit[2], CultureInfo.InvariantCulture));

                    if (Math.Abs(radius) < 0.0000000001 | radius < 0)
                    {
                        throw new BadCircleRadiusException(radius.ToString());
                    }
                    _circle = new CircleFigure(center, radius);
                //}
                //catch
                //{
                //    throw new BadFormatException(line);
                //}
                
            }
            else
            {
                throw new BadFormatException(line);
            }
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _circle);
    }
}