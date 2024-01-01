using Microsoft.AspNetCore.Identity;

namespace ProiectMPD.Models
{
    public class Library
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // Foreign key to User
        public string UserId { get; set; }
        // Navigation property to User
        public IdentityUser User { get; set; }
        // Navigation properties for many-to-many relationship with Release
        public ICollection<Release> Releases { get; set; }
    }
}
