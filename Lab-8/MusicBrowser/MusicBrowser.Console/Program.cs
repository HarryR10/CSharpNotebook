namespace MusicBrowser.Console
{
    using System.Configuration;
    using MusicBrowser.Console.Application;
    using MusicBrowser.Console.DataAccess;
    using MusicBrowser.Console.DataAccess.AdoNet;

    class Program
    {
        public static void Main()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

            IMusicRepository musicRepository = new AdoNetMusicRepository(connectionString);

            var dataModel = new MusicListModel(musicRepository);
            var list = new ExpandableList(dataModel);
            list.Run();
        }
    }
}
