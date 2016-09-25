namespace TidalExplorer.Models.Tidal
{
    public class ImageViewModel
    {
        public string ImageId { get; set; }
        public string Dimension { get; set; }
        public string CssClasses { get; set; }
    }

    public class TidalImageDimension
    {
        public static string ProfilePic => "210x210";
        public static string PlaylistPic => "320x214";
        public static string AlbumCover => "320x320";
    }
}