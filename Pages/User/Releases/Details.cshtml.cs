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
    public class DetailsModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;
        public bool IsInLibrary { get; set; }

        MusicLibraryService _musicLibraryService;

        public DetailsModel(MusicLibraryService musicLibraryService,ProiectMPD.Data.ApplicationDbContext context)
        {
            _musicLibraryService = musicLibraryService;
            _context = context;
        }

        public Release Release { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Releases
                .Include(r=>r.Artist)
                .Include(r=>r.Songs)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (release == null)
            {
                return NotFound();
            }
            else
            {
                Release = release;
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IsInLibrary = await _context.MusicLibraries
                                .Where(l => l.UserId == userId)
                                .SelectMany(l => l.Releases)
                                .AnyAsync(r => r.ID == id);
            return Page();
        }
        public async Task<IActionResult> OnPostAddToLibraryAsync(int releaseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var success = await _musicLibraryService.AddReleaseToLibrary(userId, releaseId);

            if (success)
            {
                TempData["SuccessMessage"] = "Release added to library.";
            } else
            {
                TempData["ErrorMessage"] = "Unable to add release to library.";
            }
            return RedirectToPage("./Details", new { id = releaseId });
        }
        public async Task<IActionResult> OnPostRemoveFromLibraryAsync(int releaseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var success = await _musicLibraryService.RemoveReleaseFromLibrary(userId, releaseId);

            if (success)
            {
                TempData["SuccessMessage"] = "Release removed from your library.";
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to add release to library.";
            }
            return RedirectToPage("./Details", new { id = releaseId });
        }
    }
}
