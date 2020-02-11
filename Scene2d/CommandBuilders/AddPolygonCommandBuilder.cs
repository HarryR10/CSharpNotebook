using System;
using System.Collections.Generic;
using Scene2d.Exceptions;

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
        /* Should be set in AppendLine method */
        private IFigure _polygon;

        private List<ScenePoint> AllPoints = new List<ScenePoint>();

        private bool _consolidated = false;
        private bool isBegin = true;

        /* Should be set in AppendLine method */
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
            // check if line matches the RecognizeRegex
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
                if (AllPoints.Count < 3)
                {
                    throw new BadPolygonPointNumber("bad polygon point number");
                }
                _polygon = new PolygonFigure(AllPoints.ToArray());
                
                _consolidated = true;
            }

            if (matchPoint.Success & !isBegin & matchPoint.Value != "end polygon")
            {
                string[] afterSplit = matchPoint.Value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                AllPoints.Add(new ScenePoint(double.Parse(afterSplit[2], CultureInfo.InvariantCulture), double.Parse(afterSplit[3], CultureInfo.InvariantCulture)));
            }

            if(_name == null & !isBegin)
            {
                throw new BadFormatException("name is not specified");
            }
            //else
            //{
            //    throw new BadFormatException("error in line");
            //}
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _polygon);
    }
}