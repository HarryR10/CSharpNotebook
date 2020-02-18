using System.Collections.Generic;

namespace Scene2d.Commands
{
    public class GroupCommand : ICommand
    {
        private readonly string _name;

        private IList<string> _childFigures;

        public GroupCommand(string name, IList<string> childFigures)
        {
            _name = name;
            _childFigures = childFigures;
        }

        public void Apply(Scene scene)
        {
            scene.CreateCompositeFigure(_name, _childFigures);
        }

        public string FriendlyResultMessage
        {
            get { return "Group " + _name + " was created"; }
        }
    }
}