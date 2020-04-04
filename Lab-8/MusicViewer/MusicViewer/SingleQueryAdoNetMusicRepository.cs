using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MusicViewer
{
    class SingleQueryAdoNetMusicRepository : IMusicRepository
    {
        private string _connectionString = String.Empty;
        public SingleQueryAdoNetMusicRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Album> ListAlbums()
        {
            IList<Album> results = new List<Album>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT [alb].*, " +
                                            "[sng].albumId, " +
                                            "[sng].title songname, " +
                                            "[sng].duration " +
                                            "FROM [albums] [alb] " +
                                            "RIGHT JOIN [songs] [sng] " +
                                            "ON [alb].albumId = [sng].albumId " +
                                            "ORDER BY [alb].albumId", 
                                            connection);

                using (var dataReader = command.ExecuteReader())
                {
                    var crntAlbum = new Album();
                    int? lastId = null;

                    while (dataReader.Read())
                    {
                        var albumId = (int)dataReader["albumId"];
                        if (lastId == null || lastId < albumId)
                        {
                            if(crntAlbum.Id == lastId)
                                results.Add(crntAlbum);

                            lastId = albumId;

                            crntAlbum = new Album(
                                albumId,
                                (DateTime)dataReader["date"],
                                (string)dataReader["title"],
                                new List<Song>());
                        }

                        crntAlbum.AddSong(
                            (int)dataReader["albumId"],
                            (string)dataReader["songname"],
                            (TimeSpan)dataReader["duration"]);
                    }
                    results.Add(crntAlbum);
                }
            }
            return results;
        }
    }
}
