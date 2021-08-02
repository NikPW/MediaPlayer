// Davlatov "NikPW" Farrukh
// Евгений, мы старались. Очень. Осталось немного, но вышло - как вышло. Ещё допилим. На оценку плевать.
// Пока есть поддержка только одного альбома, который надо пихать в директорию D:\Music

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using ITStepMusicPlayer.Services;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ITStepMusicPlayer {
    public partial class MainWindow : Window {

        #region Fields

        private List<Author> _authors;
        private Author _currentAuthor;
        private Track _currentTrack;

        #endregion
        #region Constructors

        public MainWindow() {
            InitializeComponent();

            _authors = AddTracks.GetAuthors();
        }

        #endregion
        #region Event Methods

        private void PlayButton_OnClick(object sender, RoutedEventArgs e) {

            if (_currentTrack == null) {
                _currentTrack = _authors[0].Albums[0].Tracks[0];
            }
            
            if (_currentTrack.IsPlay) {
                _currentTrack.Pause();
            }
            else {
                _currentTrack.Play();
            }

            TrackNameLabel.Text = _currentTrack.TrackName;

            TPSUpdateEvent?.Invoke();
        }
        private void TrackPlayButton_OnClick(object sender, RoutedEventArgs e) {

            if (((TextBlock) (((Button) sender).Content)).Text == "Исполнители") {
                List<TrackLabel> tl = new List<TrackLabel>();

                foreach (var i in _authors) {
                    tl.Add(new TrackLabel() { Name = i.Name });
                }

                TrackListXaml.ItemsSource = tl;
            }
            else if (((TextBlock) (((Button) sender).Content)).Text == "Все треки") {
                List<TrackLabel> tl = new List<TrackLabel>();

                foreach (var i in _authors[0].Albums[0].Tracks) {
                    tl.Add(new TrackLabel() { Name = i.TrackName });
                }

                TrackListXaml.ItemsSource = tl;
            }

            foreach (var i in _authors[0].Albums[0].Tracks) {
                i.Stop();
            }
            foreach (var i in _authors[0].Albums[0].Tracks) {
                if (((TextBlock)(((Button) sender).Content)).Text == i.TrackName) {
                    _currentTrack = i;
                    TrackNameLabel.Text = _currentTrack.TrackName;
                    TPSUpdateEvent?.Invoke();
                    i.Play();
                    return;
                }
            }
        }
        private void TrackProgressSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            _currentTrack.Seek(TimeSpan.FromSeconds(TrackProgressSlider.Value));
        }
        private void SliderPosition_ValueChanged(object sender, RoutedEventArgs e) {
            _currentTrack.Seek(TimeSpan.FromSeconds(TrackProgressSlider.Value));
        }
        private void TrackVolumeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            _currentTrack.Player.Volume = TrackVolumeSlider.Value;
        }
        private void SliderVolumePosition_ValueChanged(object sender, RoutedEventArgs e) {
            _currentTrack.Player.Volume = TrackVolumeSlider.Value;
        }
        private void BackwardTrack_OnClick(object sender, RoutedEventArgs e) {
            _currentTrack.Stop();
            int i;
            for (i = 0; i < _authors[0].Albums[0].Tracks.Count; ++i) {
                
                if (_currentTrack == _authors[0].Albums[0].Tracks[i]) {
                    --i;
                    if (i < 0) {
                        i = _authors[0].Albums[0].Tracks.Count - 1;
                    }
                    break;
                }
            }
            _currentTrack = _authors[0].Albums[0].Tracks[i];
            _currentTrack.Play();
        }
        private void ForwardTrack_OnClick(object sender, RoutedEventArgs e) {
            _currentTrack.Stop();
            for (int i = 0; i < _authors[0].Albums[0].Tracks.Count; ++i) {
                if (_currentTrack == _authors[0].Albums[0].Tracks[i]) {
                    if (i >= _authors[0].Albums[0].Tracks.Count - 1) {
                        i = 0;
                    }
                    else {
                        ++i;
                    }

                    _currentTrack = _authors[0].Albums[0].Tracks[i];
                    _currentTrack.Play();
                    break;
                }
            }
        }
        private void MuteButton_OnClick(object sender, RoutedEventArgs e) {
            if (_currentTrack.Player.Volume == 0) {
                _currentTrack.Player.Volume = 1;
            }
            else {
                _currentTrack.Player.Volume = 0;
            }
        }
        private void AuthorsButton_OnClick(object sender, RoutedEventArgs e) {

            List<TrackLabel> tl = new List<TrackLabel>();

            foreach (var i in _authors) {
                tl.Add(new TrackLabel() { Name = i.Name });
            }

            TrackListXaml.ItemsSource = tl;
        }
        private void TracksButton_OnClick(object sender, RoutedEventArgs e) {
            
            List<TrackLabel> tl = new List<TrackLabel>();

            foreach (var i in _authors[0].Albums[0].Tracks) {
                tl.Add(new TrackLabel() { Name = i.TrackName });
            }

            TrackListXaml.ItemsSource = tl;
        }

        #endregion
        #region Delegates

        public delegate void TPSDelegate();

        #endregion
        #region Events

        public event TPSDelegate TPSUpdateEvent;

        #endregion
        #region Methods

        public void TPSUpdate() {
            TrackProgressSlider.Maximum = _currentTrack.Player.NaturalDuration.TimeSpan.TotalSeconds;
        }

        #endregion
    }
    public class TrackLabel {
        
        public Image TrackPoster { get; set; }
        public string Name { get; set; }
        
    }
}
