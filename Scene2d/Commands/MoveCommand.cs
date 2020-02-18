namespace Scene2d.Commands
{
    public class MoveCommand : ICommand
    {
        private readonly string _name;

        private readonly ScenePoint _vector;

        public MoveCommand(string name, ScenePoint vector)
        {
            _name = name;
            _vector = vector;
        }

        public void Apply(Scene scene)
        {
            scene.Move(_name, _vector);
        }

        public string FriendlyResultMessage
        {
            get { return "Figure(s) " + _name + " was moved"; }
        }
    }
}
