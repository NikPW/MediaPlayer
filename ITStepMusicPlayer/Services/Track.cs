using System;
using System.IO;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TagLib;
using ITStepMusicPlayer.Services.Interfaces;

namespace ITStepMusicPlayer.Services {
    public record Track : IMedia {
        
        #region Properties

        public string Reference { get; set; }
        public string Genre { get; }
        public Image Poster { get; }
        public DateTime ReleaseDate { get; }
        public Author Author { get; }
        private MediaPlayer _player;

        #endregion
        #region Constructors
            
        public Track(string reference) {

            Reference = reference;
                
            var file = TagLib.File.Create(Reference);
            Genre = file.Tag.FirstGenre;

            /*
            Image bm = new Image();
            bm.Comtent
            var ms = new MemoryStream();
            bm.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            */

            if (file.Tag.DateTagged.HasValue) {
                ReleaseDate = file.Tag.DateTagged.Value;
            }
            else {
                ReleaseDate = new DateTime(1970, 1, 1);
            }

            Author = new Author(file.Tag.FirstAlbumArtist);
            _player.Open(new Uri("file:///" + Reference, UriKind.Absolute));
            _player.Play();
        }

        #endregion
        #region Methods

        public void Play() {
                
        }
            
        #endregion
    }
}
