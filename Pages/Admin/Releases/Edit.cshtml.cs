using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.Releases
{
    public class EditModel : ReleaseGenresPageModel 
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public EditModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Release Release { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release =  await _context.Releases.FirstOrDefaultAsync(m => m.ID == id);
            Release = await _context.Releases
             .Include(b => b.Artist)
             .Include(b => b.ReleaseGenres).ThenInclude(b => b.Genre)
             .AsNoTracking()
             .FirstOrDefaultAsync(m => m.ID == id);
            if (Release == null)
            {
                return NotFound();
            }
            PopulateAssignedGenreData(_context, Release);

            ViewData["ArtistID"] = new SelectList(_context.Artists, "ID", "Name");


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedGenres)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se va include Author conform cu sarcina de la lab 2
            var releaseToUpdate = await _context.Releases
            .Include(i => i.Artist)
            .Include(i => i.ReleaseGenres)
            .ThenInclude(i => i.Genre)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (releaseToUpdate == null)
            {
                return NotFound();
            }
            //se va modifica AuthorID conform cu sarcina de la lab 2
            if (await TryUpdateModelAsync<Release>(
            releaseToUpdate,
            "Release",
            i => i.Name,
            i => i.Type, i => i.Language,
            i => i.Label,i=> i.ArtistID,  i => i.Year))
            {
                UpdateReleaseGenres(_context, selectedGenres, releaseToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care 
            //este editata 
            UpdateReleaseGenres(_context, selectedGenres, releaseToUpdate);
            PopulateAssignedGenreData(_context, releaseToUpdate);
            return Page();
        }
    }
}
