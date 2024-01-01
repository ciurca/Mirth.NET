using Microsoft.AspNetCore.Identity;

namespace ProiectMPD.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; } // This should match the type of your User ID
        public IdentityUser User { get; set; }

        // Foreign key to Release
        public int ReleaseID { get; set; }
        // Navigation property to Release
        public Release Release { get; set; }
    }
}
