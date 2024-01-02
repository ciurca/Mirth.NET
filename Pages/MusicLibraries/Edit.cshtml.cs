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

namespace ProiectMPD.Pages.MusicLibraries
{
    public class EditModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public EditModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MusicLibrary MusicLibrary { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musiclibrary =  await _context.MusicLibraries.FirstOrDefaultAsync(m => m.ID == id);
            if (musiclibrary == null)
            {
                return NotFound();
            }
            MusicLibrary = musiclibrary;
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MusicLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicLibraryExists(MusicLibrary.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MusicLibraryExists(int id)
        {
            return _context.MusicLibraries.Any(e => e.ID == id);
        }
    }
}
