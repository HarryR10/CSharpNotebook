namespace Scene2d.CommandBuilders
{
    using Scene2d.Commands;

    public class AddCommentCommandBuilder : ICommandBuilder
    {
        private string _comment;

        public bool IsCommandReady
        {
            get
            {
                return true;
            }
        }

        public void AppendLine(string line)
        {
            char[] sharp = "#".ToCharArray();
            _comment = line.TrimStart(sharp).TrimStart();
        }

        public ICommand GetCommand() => new DoNothing(_comment);
    }
}