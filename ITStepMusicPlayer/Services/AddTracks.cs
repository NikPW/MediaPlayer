// Davlatov "NikPW" Farrukh

using System.Collections.Generic;

namespace ITStepMusicPlayer.Services {
    public static class AddTracks {
        public static List<Author> GetAuthors() {

            var tracksDirectories = SearchTracks.Search();

            List<Track> tracks = new List<Track>();
            List<Album> albums = new List<Album>();
            List<Author> authors = new List<Author>();

            foreach (var i in tracksDirectories) {
                tracks.Add(new Track(i));
            }

            foreach (var i in tracksDirectories) {
                int b = i.LastIndexOf(@"\");
                string a = tracksDirectories[0].Substring(0, b);
                albums.Add(new Album(a));
            }
            
            foreach (var i in albums) {
                authors.Add(new Author(i.Author, i));
            }

            return authors;
        }
    }
}
