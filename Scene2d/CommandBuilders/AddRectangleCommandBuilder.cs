using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Figures;

    public class AddRectangleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex(@"(\w*\-*)+\s+(\(\-?(\d+|\d+\.\d+|\d+\,\d+)\,\s?\-?(\d+|\d+\.\d+|\d+\,\d+)\)\s?){2}");
        private IFigure _rectangle;

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

                //try
                //{
                    _name = afterSplit[0];

                    var firstPoint = new ScenePoint(double.Parse(afterSplit[1], CultureInfo.InvariantCulture), double.Parse(afterSplit[2], CultureInfo.InvariantCulture));
                    var secondPoint = new ScenePoint(double.Parse(afterSplit[3], CultureInfo.InvariantCulture), double.Parse(afterSplit[4], CultureInfo.InvariantCulture));

                    if (firstPoint.X == secondPoint.X | firstPoint.Y == secondPoint.Y)
                    {
                        throw new BadRectanglePointException(string.Format("({0}, {1}) ({2}, {3})", afterSplit[1], afterSplit[2], afterSplit[3], afterSplit[4]));
                    }

                    _rectangle = new RectangleFigure(firstPoint, secondPoint);
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

        public ICommand GetCommand() => new AddFigureCommand(_name, _rectangle);
    }
}