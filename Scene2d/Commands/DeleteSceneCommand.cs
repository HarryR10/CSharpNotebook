namespace Scene2d.Commands
{
    public class DeleteSceneCommand : ICommand
    {
        //public DeleteSceneCommand()
        //{
        //}

        public void Apply(Scene scene)
        {
            scene.DeleteScene();
        }

        public string FriendlyResultMessage
        {
            get { return "Scene is clear"; }
        }
    }
}
