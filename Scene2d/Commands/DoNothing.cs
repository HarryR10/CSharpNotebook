namespace Scene2d.Commands
{
    public class DoNothing : ICommand
    {
        string _message;

        public DoNothing(string message)
        {
            _message = message;
        }

        public void Apply(Scene scene)
        {
        }

        public string FriendlyResultMessage
        {
            get { return "Added undefined string: " + _message; }
        }
    }
}