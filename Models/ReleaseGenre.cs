namespace ProiectMPD.Models
{
    public class ReleaseGenre
    {
        public int ID { get; set; }
        public int ReleaseID { get; set; }
        public Release Release { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}
