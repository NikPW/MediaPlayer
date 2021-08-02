using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ITStepMusicPlayer.Services.Interfaces;
using Image = System.Windows.Controls.Image;

namespace ITStepMusicPlayer.Services {
    public record Album : IMedia {

        #region Fields

        private List<Track> _tracks;

        #endregion
        #region Properties

        public string Reference { get; set; }
        public string AlbumName { get; }
        public string Genre { get; }
        public Image Poster { get; }
        public DateTime ReleaseDate { get; }
        public string Author { get; }
        public List<Track> Tracks { get => _tracks; }

        #endregion
        #region Constructors

        public Album(string directory) {

            Reference = directory;
            _tracks = new List<Track>();
            DirectoryInfo di = new DirectoryInfo(directory);
            var files = di.GetFiles();
            
            foreach (var i in files) {
                if (i.FullName.Contains("jpg") || i.FullName.Contains("png")) {
                    Poster = new Image();
                    Poster.Source = new BitmapImage(new Uri(i.FullName));
                }
            }

            var file = TagLib.File.Create(Reference + "/" + files[0].Name);
            Genre = file.Tag.FirstGenre;

            foreach (var i in files) {
                if (i.FullName.Contains("mp3")) {
                    _tracks.Add(new Track(Reference + "/" + i.Name));
                }
            }
            
            if (file.Tag.DateTagged.HasValue) {
                ReleaseDate = file.Tag.DateTagged.Value;
            }
            else {
                ReleaseDate = new DateTime(1970, 1, 1);
            }

            AlbumName = di.Name;
            Author = file.Tag.FirstAlbumArtist;
        }
        
        #endregion
    }
}
