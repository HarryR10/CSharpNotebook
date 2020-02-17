using System;
using System.Collections.Generic;
using Scene2d.Exceptions;
using Scene2d.MathLibs;

namespace Scene2d.CommandBuilders
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Figures;

    public class AddPolygonCommandBuilder : ICommandBuilder
    {
        private static readonly Regex NameRegex = new Regex(@"add polygon\s+((\w*\-*)+\s?)");
        private static readonly Regex RecognizeRegex = new Regex(@"add point\s+\(\-?(\d+|\d+\.\d+|\d+\,\d+)\,\s?\-?(\d+|\d+\.\d+|\d+\,\d+)\)|end polygon");

        private IFigure _polygon;

        private List<ScenePoint> allPoints = new List<ScenePoint>();

        private bool _consolidated = false;
        private bool isBegin = true;

        private string _name;

        public bool IsCommandReady
        {
            get
            {
                if (_consolidated)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void AppendLine(string line)
        {
            //try
            //{
                var matchName = NameRegex.Match(line);
                string[] separators = { " ", "(", ",", ")" };

                if (matchName.Success & isBegin)
                {
                    string[] afterSplit = matchName.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    _name = afterSplit[2];
                    isBegin = false;
                }

                var matchPoint = RecognizeRegex.Match(line);

                if (matchPoint.Success & !isBegin & matchPoint.Value == "end polygon")
                {
                    if (allPoints.Count < 3)
                    {
                        throw new BadPolygonPointNumberException("bad polygon point number");
                    }
                    _polygon = new PolygonFigure(allPoints.ToArray());

                    _consolidated = true;
                }

                if (matchPoint.Success & !isBegin & matchPoint.Value != "end polygon")
                {
                    string[] afterSplit = matchPoint.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    ScenePoint currentPoint;

                    try
                    {
                        currentPoint = new ScenePoint(double.Parse(afterSplit[2], CultureInfo.InvariantCulture),
                            double.Parse(afterSplit[3], CultureInfo.InvariantCulture));
                    }
                    catch
                    {
                        throw new BadFormatException(line);
                    }

                    foreach (var el in allPoints)
                    {
                        // ICompare?
                        if (el.X == currentPoint.X & el.Y == currentPoint.Y)
                        {
                           throw new BadPolygonPointException(currentPoint.ToString() +
                            " point coordinates is coincides with one or more points, which already added");
                        }
                    }

                if (allPoints.Count > 1)
                    {
                        PolygonMath.CheckIntersection(allPoints, currentPoint);
                    }

                    allPoints.Add(currentPoint);
                }

                if (_name == null & !isBegin)
                {
                    throw new BadFormatException(line);
                }
            //}
            //catch
            //{
            //    throw new BadFormatException(line);
            //}
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _polygon);
    }
}