// Davlatov "NikPW" Farrukh

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ITStepMusicPlayer.Services;

namespace ITStepMusicPlayer {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Track t = new Track("C:/Users/user/Desktop/Powerwolf - Lust for blood.mp3");

            List<TrackLabel> tl = new List<TrackLabel>();
            tl.Add(new TrackLabel() { TrackPoster = t.Poster, TrackName = t.TrackName });

            TrackListXaml.ItemsSource = tl;
        }
    }

    public class TrackLabel {
        
        public Image TrackPoster { get; set; }
        public string TrackName { get; set; }
        
    }
}
