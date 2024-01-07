using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ProiectMPD.Models
{
    public class Release
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^(Single|EP|Album|Mixtape)$", ErrorMessage = "Enter a valid Type. Types allowed: Single, EP, Album or Mixtape")]
        public string Type { get; set; }
        public string? Language { get; set; }
        [Url(ErrorMessage = "Enter a valid URL pointing to the Artwork of the release.")]
        public string? Artwork { get; set; }
        public string? Label { get; set; }
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Enter a valid Year.")]
        public int? Year { get; set; }
        public ICollection<ReleaseGenre>? ReleaseGenres { get; set; }
        public int? ArtistID { get; set; }
        public Artist? Artist { get; set; }
        public ICollection<Song>? Songs { get; set; }
        public ICollection<MusicLibrary>? MusicLibraries { get; set; }

        public ICollection<Review>? Reviews { get; set; }


    }
}
