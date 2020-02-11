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
                // как работает этот словарь?
                // () => new AddRectangleCommandBuilder() - объявление функции и тут же ее
                // добавление в словарь?
                { new Regex("^add rectangle .*"), () => new AddRectangleCommandBuilder() },
                { new Regex("^add circle .*"), () => new AddСircleCommandBuilder() },
                { new Regex("^add polygon .*"), () => new AddPolygonCommandBuilder() },
                { new Regex("^add point .*"), () => new AddPolygonCommandBuilder() },
                { new Regex("^end polygon"), () => new AddPolygonCommandBuilder() },
                { new Regex("^move .*"), () => new MoveFigureCommandBuilder() },
                { new Regex("^delete .*"), () => new DeleteFigureCommandBuilder() }
                // todo: Should add new Regex for #comments
            };

        private ICommandBuilder _currentBuilder;

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
                    throw new BadFormatException("error in line");
                }
            }
            _currentBuilder.AppendLine(line);
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
