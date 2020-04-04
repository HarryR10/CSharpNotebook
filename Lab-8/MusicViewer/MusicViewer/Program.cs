using Microsoft.Extensions.Configuration;
using System;

namespace MusicViewer
{
    class Program
    {
        static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            string connectionString = config.GetConnectionString("DefaultConnectionString");
            IMusicRepository musicRepository = new AdoNetMusicRepository(connectionString);
            Console.WriteLine("Reading albums:");
            Console.WriteLine();

            var defaultColor = Console.ForegroundColor;
            #region OldCode
            /*foreach (Album album in musicRepository.ListAlbums())
            {
                var formattedAlbumSummary = string.Format(
                "#{0} {1:yyyy-MM-dd} {2}",
                album.Id,
                album.Date,
                album.Title);
                Console.WriteLine(formattedAlbumSummary);
                Console.WriteLine();
                foreach (Song song in album.Songs)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" " + song.Title.PadRight(Console.WindowWidth - 12));
                    Console.ForegroundColor = defaultColor;
                    Console.WriteLine(string.Format("{0:mm\\:ss}", song.Duration));
                }
                Console.WriteLine();
            }
            Console.WriteLine("Done");*/
            #endregion

            IMusicRepository anotherMusicRepository = new SingleQueryAdoNetMusicRepository(connectionString);
            foreach (Album album in anotherMusicRepository.ListAlbums())
            {
                var formattedAlbumSummary = string.Format(
                "#{0} {1:yyyy-MM-dd} {2}",
                album.Id,
                album.Date,
                album.Title);
                Console.WriteLine(formattedAlbumSummary);
                Console.WriteLine();
                foreach (Song song in album.Songs)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" " + song.Title.PadRight(Console.WindowWidth - 12));
                    Console.ForegroundColor = defaultColor;
                    Console.WriteLine(string.Format("{0:mm\\:ss}", song.Duration));
                }
                Console.WriteLine();
            }
            Console.WriteLine("Done");
        }
    }
}