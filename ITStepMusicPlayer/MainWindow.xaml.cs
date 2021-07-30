using System.Windows;
using ITStepMusicPlayer.Services;

namespace ITStepMusicPlayer {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            Track t = new Track("C:/Users/user/Desktop/Skillet Hero.mp3");
        }
    }
}
