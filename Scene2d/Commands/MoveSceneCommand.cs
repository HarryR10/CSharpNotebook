namespace Scene2d.Commands
{
    using Scene2d.Figures;

    public class MoveSceneCommand : ICommand
    {
        private readonly ScenePoint _vector;

        public MoveSceneCommand(ScenePoint vector)
        {
            _vector = vector;
        }

        public void Apply(Scene scene)
        {
            scene.MoveScene(_vector);
        }

        public string FriendlyResultMessage
        {
            get { return "Scene was moved"; }
        }
    }
}