namespace Scene2d.Commands
{
    public class ReflectSceneCommand : ICommand
    {
        private readonly ReflectOrientation _reflectOrientation;

        public ReflectSceneCommand(ReflectOrientation reflectOrientation)
        {
            _reflectOrientation = reflectOrientation;
        }

        public void Apply(Scene scene)
        {
            scene.ReflectScene(_reflectOrientation);
        }

        public string FriendlyResultMessage
        {
            get
            {
                return "Scene was reflected at "
                  + _reflectOrientation;
            }
        }
    }
}