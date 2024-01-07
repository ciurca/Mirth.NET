using Microsoft.AspNetCore.Identity;

namespace ProiectMPD.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        public int? ReleaseID { get; set; }
        public Release? Release { get; set; }
    }
}
