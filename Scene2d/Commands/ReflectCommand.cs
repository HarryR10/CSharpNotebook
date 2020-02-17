namespace Scene2d.Commands
{
    public class ReflectCommand : ICommand
    {
        private readonly string _name;

        private readonly ReflectOrientation _reflectOrientation;

        public ReflectCommand(string name, ReflectOrientation reflectOrientation)
        {
            _name = name;
            _reflectOrientation = reflectOrientation;
        }

        public void Apply(Scene scene)
        {
            scene.Reflect(_name, _reflectOrientation);
        }

        public string FriendlyResultMessage
        {
            get
            {
                return "Figure(s) " + _name + " was reflected at "
                  + _reflectOrientation;
            }
        }
    }
}