using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.Releases
{
    public class CreateModel : ReleaseGenresPageModel 
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public CreateModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var artistList = _context.Artists.OrderBy(a => a.Name).ToList();
            ViewData["ArtistID"] = new SelectList(artistList, "ID", "Name");

            var release = new Release();
            release.ReleaseGenres = new List<ReleaseGenre>();
            PopulateAssignedGenreData(_context, release);

            return Page();
        }

        [BindProperty]
        public Release Release { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedGenres)
        {
            var newRelease = new Release();
            if (selectedGenres != null)
            {
                newRelease.ReleaseGenres = new List<ReleaseGenre>();
                foreach (var gen in selectedGenres)
                {
                    var genToAdd = new ReleaseGenre 
                    {
                        GenreID = int.Parse(gen)
                    };
                    newRelease.ReleaseGenres.Add(genToAdd);
                }
            }
            Release.ReleaseGenres= newRelease.ReleaseGenres;
            _context.Releases.Add(Release);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
