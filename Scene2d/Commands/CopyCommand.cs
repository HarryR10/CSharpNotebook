namespace Scene2d.Commands
{
    public class CopyCommand : ICommand
    {
        private readonly string _name;

        private string _copyName;

        public CopyCommand(string name, string copyName)
        {
            _name = name;
            _copyName = copyName;
        }

        public void Apply(Scene scene)
        {
            scene.Copy(_name, _copyName);
        }

        public string FriendlyResultMessage
        {
            get { return "Figure(s) " + _name + " was copied to " + _copyName; }
        }
    }
}