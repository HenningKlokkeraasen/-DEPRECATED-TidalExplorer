using System.Threading.Tasks;
using System.Web.Mvc;
using TidalExplorer.Models.Tidal;
using TidalExplorer.TidalIntegration;

namespace TidalExplorer.Controllers
{
    public class TidalUserPlaylistsController : Controller
    {
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var session = await OpenTidlIntegrator.RestoreSessionFromClaimsIdentity(User.Identity);

            if (session == null)
                return new EmptyResult();

            var playlists = await session.GetUserPlaylists();

            if (playlists == null)
                return new EmptyResult();

            return View("Index", playlists);
        }

        [Authorize]
        public async Task<ActionResult> Playlist(string playlistUuId)
        {
            var session = await OpenTidlIntegrator.RestoreSessionFromClaimsIdentity(User.Identity);

            if (session == null)
                return new EmptyResult();

            var playlist = await session.GetPlaylist(playlistUuId);

            if (playlist == null)
                return new EmptyResult();

            var tracks = await session.GetPlaylistTracks(playlistUuId);

            var playlistViewModel = new PlaylistViewModel
            {
                Playlist = playlist,
                Tracks = tracks
            };

            return View("Playlist", playlistViewModel);
        }
    }
}