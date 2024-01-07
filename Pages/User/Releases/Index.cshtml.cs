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
using ProiectMPD.Services;

namespace ProiectMPD.Pages.User.Releases
{
    public class IndexModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;
        private readonly MusicLibraryService _musicLibraryService;
        public HashSet<int> ReleasesInLibrary { get; set; }

        public string CurrentFilter { get; set; }

        public IndexModel(MusicLibraryService musicLibraryService,ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
            _musicLibraryService = musicLibraryService;
        }

        public IList<Release> Release { get;set; } = default!;

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;
            Release = await _context.Releases
                .Include(r => r.Artist).ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                Release = await _context.Releases.Where(s => s.Artist.Name.Contains(searchString)
               || s.Name.Contains(searchString)).ToListAsync();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var libraryReleaseIds = await _context.MusicLibraries
                                          .Where(l => l.UserId == userId)
                                          .SelectMany(l => l.Releases)
                                          .Select(r => r.ID)
                                          .ToListAsync();
            ReleasesInLibrary = libraryReleaseIds.ToHashSet();

        }
        public async Task<IActionResult> OnPostAddToLibraryAsync(int releaseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _musicLibraryService.AddReleaseToLibrary(userId, releaseId);

            if (success)
            {
                TempData["SuccessMessage"] = "Release added to your library.";
                return RedirectToPage("./Index");
            }
            TempData["ErrorMessage"] = "Unable to add release to library.";
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostRemoveFromLibraryAsync(int releaseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _musicLibraryService.RemoveReleaseFromLibrary(userId, releaseId);

            if (success)
            {
                TempData["SuccessMessage"] = "Release removed from your library.";
                return RedirectToPage("./Index");
            }
            TempData["ErrorMessage"] = "Unable to remove release to library.";
            return RedirectToPage();
        }

    }
}
