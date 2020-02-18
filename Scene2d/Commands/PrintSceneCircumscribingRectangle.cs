namespace Scene2d.Commands
{
    public class PrintSceneCircumscribingRectangle : ICommand
    {
        private string _message;

        public void Apply(Scene scene)
        {
            _message = scene.PrintSceneCircumscribingRectangle();
        }

        public string FriendlyResultMessage
        {
            get
            {
                return _message + " - is coordinates of scene's circumscribing rectangle";
            }
        }
    }
}