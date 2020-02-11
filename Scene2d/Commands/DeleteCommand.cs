namespace Scene2d.Commands
{
    public class DeleteCommand : ICommand
    {
        private readonly string _name;

        public DeleteCommand(string name)
        {
            _name = name;
        }

        public void Apply(Scene scene)
        {
            scene.Delete(_name);
        }

        public string FriendlyResultMessage
        {
            get { return "Figure " + _name + " was deleted"; }
        }
    }
}