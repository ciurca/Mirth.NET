namespace ProiectMPD.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public ICollection<Release>? Releases { get; set; }
    }
}
