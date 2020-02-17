namespace Scene2d.Commands
{
    public class PrintCircumscribingRectangle : ICommand
    {
        private readonly string _name;

        private string _message;

        public PrintCircumscribingRectangle(string name)
        {
            _name = name;
        }

        public void Apply(Scene scene)
        {
            _message = scene.PrintCircumscribingRectangle(_name);
        }

        public string FriendlyResultMessage
        {
            get
            {
                return _message + " - is coordinates of figure's circumscribing rectangle" + _name;
            }
        }
    }
}