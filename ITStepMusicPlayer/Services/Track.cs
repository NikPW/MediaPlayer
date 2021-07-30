// Davlatov "NikPW" Farrukh

using System;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TagLib;
using ITStepMusicPlayer.Services.Interfaces;
using Image = System.Windows.Controls.Image;

namespace ITStepMusicPlayer.Services {
    public record Track : IMedia {
        
        #region Properties
        
        public string Reference { get; set; }
        public string TrackName { get; }
        public string Genre { get; }
        public Image Poster { get; }
        public DateTime ReleaseDate { get; }
        public string Author { get; }
        public string Album { get; }
        public bool IsPlay { get; private set; }
        public MediaPlayer Player { get; }


        #endregion
        #region Constructors
            
        public Track(string reference) {

            Reference = reference;
                
            var file = TagLib.File.Create(Reference);
            TrackName = file.Tag.Title;
            Genre = file.Tag.FirstGenre;

            // Getting poster from ID3
            try {
                TagLib.IPicture pic = file.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                ms.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.EndInit();

                Poster.Source = bitmap;
            }
            catch {
                Poster = new Image();
                Poster.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/DefaultTrackPoster.jpg"));
            }

            if (file.Tag.DateTagged.HasValue) {
                ReleaseDate = file.Tag.DateTagged.Value;
            }
            else {
                ReleaseDate = new DateTime(1970, 1, 1);
            }

            Author = file.Tag.FirstAlbumArtist;
            Album = file.Tag.Album;
            Player = new MediaPlayer();
            Player.Open(new Uri("file:///" + Reference));
        }

        #endregion
        #region Methods

        public void Play() {
            Player.Play();
            IsPlay = true;
        }
        public void Pause() {
            Player.Pause();
            IsPlay = false;
        }
        public void Stop() {
            Player.Stop();
            IsPlay = false;
        }
        public void Seek(TimeSpan t) {
            Stop();
            Player.Position = t;
            Play();
        }

        #endregion
    }
}
