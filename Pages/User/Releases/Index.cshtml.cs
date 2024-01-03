using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ProiectMPD.Pages.User.Releases
{
    public class IndexModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public IndexModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Release> Release { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Release = await _context.Releases
                .Include(r => r.Artist).ToListAsync();
        }
        public async Task<IActionResult> OnPostAddToLibraryAsync(int releaseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user's ID
            var library = await _context.MusicLibraries.FirstOrDefaultAsync(l => l.UserId == userId);
            
            if (library.Releases == null)
            {
                library.Releases = new List<Release>();
            }

            if (!library.Releases.Any(r => r.ID == releaseId))
            {
                var releaseToAdd = await _context.Releases.FindAsync(releaseId);
                library.Releases.Add(releaseToAdd);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

    }
}
