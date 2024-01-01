namespace ProiectMPD.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        // Assuming Song to Release is a one-to-many relationship
        public int ReleaseID { get; set; } // Foreign key
        public Release Release { get; set; } // Navigation property
    }
}
