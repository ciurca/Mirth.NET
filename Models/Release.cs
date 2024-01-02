using Microsoft.Extensions.DependencyModel;

namespace ProiectMPD.Models
{
    public class Release
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Language { get; set; }
        public string? Label { get; set; }
        public int? Year { get; set; }
        // Navigation properties for many-to-many relationship
        public ICollection<ReleaseGenre>? ReleaseGenres { get; set; }
        public int? ArtistID { get; set; } // Foreign key
        public Artist? Artist { get; set; } // Navigation property
        public ICollection<Song>? Songs { get; set; } // If a Release can have multiple Songs
        public ICollection<MusicLibrary>? MusicLibraries { get; set; } // For the many-to-many relationship with Library

        public ICollection<Review>? Reviews { get; set; }


    }
}
