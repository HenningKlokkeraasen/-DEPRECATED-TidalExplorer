using OpenTidl.Models;
using OpenTidl.Models.Base;

namespace TidalExplorer.Models.Tidal
{
    public class PlaylistViewModel
    {
        public PlaylistModel Playlist { get; set; }
        public JsonList<TrackModel> Tracks { get; set; }
    }
}