namespace Scene2d.Commands
{
    public class CopySceneCommand : ICommand
    {
        private string _copyName;

        public CopySceneCommand(string copyName)
        { 
            _copyName = copyName;
        }

        public void Apply(Scene scene)
        {
            scene.CopyScene(_copyName);
        }

        public string FriendlyResultMessage
        {
            get { return "Scene was copied to " + _copyName; }
        }
    }
}