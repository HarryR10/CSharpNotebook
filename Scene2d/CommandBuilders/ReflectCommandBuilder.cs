using System;
using Scene2d.Exceptions;

namespace Scene2d.CommandBuilders
{

    using System.Text.RegularExpressions;
    using Scene2d.Commands;

    public class ReflectCommandBuilder : ICommandBuilder
    {

        private static readonly Regex RecognizeRegex = new Regex(
            @"reflect\s+(vertically|horizontally)\s+((\w*\-*)+)");

        private string _name;

        private ReflectOrientation _reflectOrientation;

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

                if(afterSplit[1] == "vertically")
                {
                    _reflectOrientation = ReflectOrientation.Vertical;
                }
                else if(afterSplit[1] == "horizontally")
                {
                    _reflectOrientation = ReflectOrientation.Horizontal;
                }


                if (afterSplit[2] == "scene")
                {
                    _isScene = true;
                }
                else
                {
                    _name = afterSplit[2];
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
                //return new ReflectSceneCommand(_reflectOrientation);
            }
            return new ReflectCommand(_name, _reflectOrientation);
        }
    }
}