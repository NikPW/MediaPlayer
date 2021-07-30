// Davlatov "NikPW" Farrukh

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ITStepMusicPlayer.Services;

namespace ITStepMusicPlayer {
    public partial class MainWindow : Window {

        private Track t;
        
        public MainWindow() {
            InitializeComponent();

            t = new Track("C:/Users/user/Desktop/Powerwolf - Lust for blood.mp3");

            List<TrackLabel> tl = new List<TrackLabel>();
            tl.Add(new TrackLabel() { TrackPoster = t.Poster, TrackName = t.TrackName });

            TrackListXaml.ItemsSource = tl;
            
            TrackProgressSlider.Minimum = 0;
            TrackProgressSlider.Maximum = t.Player.NaturalDuration.TimeSpan.TotalSeconds;
        }
        
        private void PlayButton_OnClick(object sender, RoutedEventArgs e) {
            if (t.IsPlay) {
                t.Pause();
            }
            else {
                t.Play();
            }
        }

        private void TrackProgressSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            int pos = Convert.ToInt32(TrackProgressSlider.Value);
            t.Seek(new TimeSpan(0, 0, 0, pos, 0));
        }
    }

    public class TrackLabel {
        
        public Image TrackPoster { get; set; }
        public string TrackName { get; set; }
        
    }
}
