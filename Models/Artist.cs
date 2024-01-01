namespace ProiectMPD.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        // Navigation properties for many-to-many relationship
        public ICollection<Release>? Releases { get; set; }
    }
}
