namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class CommandProducer : ICommandBuilder
    {
        //Func<ICommandBuilder> - это ссылка на функцию?
        private static readonly Dictionary<Regex, Func<ICommandBuilder>> Commands =
            new Dictionary<Regex, Func<ICommandBuilder>>
            {
                { new Regex("^add rectangle .*"), () => new AddRectangleCommandBuilder() },
                { new Regex("^add circle .*"), () => new AddСircleCommandBuilder() },
                { new Regex("^add polygon .*"), () => new AddPolygonCommandBuilder() },
                //{ new Regex("^add point .*"), () => new AddPolygonCommandBuilder() },
                //{ new Regex("^end polygon"), () => new AddPolygonCommandBuilder() },
                { new Regex("^move .*"), () => new MoveFigureCommandBuilder() },
                { new Regex("^delete .*"), () => new DeleteFigureCommandBuilder() },
                //{ new Regex("^#.*"), () => new AddCommentCommandBuilder() }
            };

        private ICommandBuilder _currentBuilder;

        internal void ToNull()
        {
            _currentBuilder = null;
        }

        public bool IsCommandReady
        {
            get
            {
                if (_currentBuilder == null)
                {
                    return false;
                }

                return _currentBuilder.IsCommandReady;
            }
        }

        public void AppendLine(string line)
        {
            if (_currentBuilder == null)
            {
                foreach (var pair in Commands)
                {
                    if (pair.Key.IsMatch(line))
                    {
                        //проверяем совпадение строки на каждый
                        //элемент справочника

                        _currentBuilder = pair.Value();
                        break;
                    }
                }

                if (_currentBuilder == null)
                {
                    throw new BadFormatException(line);
                }
            }
            else if (new Regex("^#.*").IsMatch(line))
            {
                return; //это комментарий
            }
            else if (_currentBuilder.GetType() == typeof(AddPolygonCommandBuilder) &
                !(new Regex(@"^(\s*)(add point .*|end polygon)").IsMatch(line)))
            {
                throw new UnexpectedEndOfPolygonExeption("please, add more points, or finish \"add polygon\"-command");
            }


            _currentBuilder.AppendLine(line);
            //----вариант, если мы считаем добавление комментария командой------
            {
                //прежде нужно раскомментировать { new Regex("^#.*"), () => new AddCommentCommandBuilder() выше
                //также можно выделить в интерфейсе доп. поле bool isMultiStringCommand и проверять уже его, а не тип

                //else if (_currentBuilder.GetType() == typeof(AddPolygonCommandBuilder) & new Regex("^#.*").IsMatch(line))
                //{
                //    var rgx = new Regex("^#.*");
                //    Commands.TryGetValue(rgx, out var subBuilder); // почему уходит в null?
                //    _currentBuilder = subBuilder.Invoke();

                //    //после этого многострочная команда прерывается однострочной(которая выполняется!),
                //    //но нам придется вызвать ошибку
                //    throw new UnexpectedEndOfPolygonExeption("error in line");
                //}
            }
        }

        //этот метод возвращает объект с типом интерфейса
        public ICommand GetCommand()
        {
            if (_currentBuilder == null)
            {
                return null;
            }

            var command = _currentBuilder.GetCommand();
            _currentBuilder = null;

            return command;
        }
    }
}
