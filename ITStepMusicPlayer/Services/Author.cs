
using System.Collections.Generic;

namespace ITStepMusicPlayer.Services {
    public class Author {
        
        #region Properties
        
        public string Name { get; }
        public List<Album> Albums { get; }
        
        #endregion
        #region Constructors

        public Author(string authorName, Album album) {
            Name = authorName;
            Albums = new List<Album>();
            Albums.Add(album);
        }
        
        #endregion
    }
}