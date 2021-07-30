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

            for (int i = 0; i < tracks.Count; ++i) {
                for (int j = 0; j < albums.Count; ++j) {
                    if (albums[j].AlbumName.Equals(tracks[i].Album)) {
                        albums[j].Tracks.Add(tracks[i]);
                    }
                    else {
                        int indexOfLastSlash = tracksDirectories[i].LastIndexOf('/');
                        string albumDirectory = tracksDirectories[i].Substring(0, indexOfLastSlash);
                        albums.Add(new Album(albumDirectory));
                    }
                }
            }

            foreach (var i in albums) {
                for (int j = 0; j < authors.Count; ++j) {
                    if (authors[j].Name.Equals(i.Author)) {
                        authors[j].Albums.Add(i);
                    }
                    else {
                        authors.Add(new Author(i.Author, i));
                    }
                }
            }

            return authors;
        }
    }
}
