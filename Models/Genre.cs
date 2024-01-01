namespace ProiectMPD.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // Navigation properties for many-to-many relationship
        public ICollection<Release>? Releases { get; set; }
    }
}
