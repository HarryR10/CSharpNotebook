namespace Scene2d.Commands
{
    public class RotateCommand : ICommand
    {
        private readonly string _name;

        private readonly double _angle;

        public RotateCommand(string name, double angle)
        {
            _name = name;
            _angle = angle;
        }

        public void Apply(Scene scene)
        {
            scene.Rotate(_name, _angle);
        }

        public string FriendlyResultMessage
        {
            get { return "Figure(s) " + _name + " was rotated at "
                    + _angle + " degrees"; }
        }
    }
}