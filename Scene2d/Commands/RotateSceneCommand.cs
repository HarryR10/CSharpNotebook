namespace Scene2d.Commands
{
    public class RotateSceneCommand : ICommand
    {
        private readonly double _angle;

        public RotateSceneCommand(double angle)
        {
            _angle = angle;
        }

        public void Apply(Scene scene)
        {
            scene.RotateScene(_angle);
        }

        public string FriendlyResultMessage
        {
            get
            {
                return "Scene was rotated at "
                 + _angle + " degrees";
            }
        }
    }
}
