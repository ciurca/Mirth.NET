using Microsoft.AspNetCore.Identity;

namespace ProiectMPD.Models
{
    public class MusicLibrary
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Release> Releases { get; set; }
    }
}
