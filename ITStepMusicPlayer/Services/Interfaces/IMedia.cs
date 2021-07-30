using System;
using System.Windows.Controls;

namespace ITStepMusicPlayer.Services.Interfaces {
    public interface IMedia {
        
        string Reference { get; set; }
        string Genre { get; }
        Image Poster { get; }
        DateTime ReleaseDate { get; }
        Author Author { get; }
        
    }
}
