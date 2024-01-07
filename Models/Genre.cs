namespace ProiectMPD.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<ReleaseGenre>? ReleaseGenres { get; set; }
    }
}
