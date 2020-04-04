using System.Collections.Generic;

namespace MusicViewer
{
    interface IMusicRepository
    {
        IEnumerable<Album> ListAlbums();
    }
}
