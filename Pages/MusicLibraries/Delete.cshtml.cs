using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.MusicLibraries
{
    public class DeleteModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public DeleteModel(ProiectMPD.Data.ApplicationDbContext context)
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

            var musiclibrary = await _context.MusicLibraries.FirstOrDefaultAsync(m => m.ID == id);

            if (musiclibrary == null)
            {
                return NotFound();
            }
            else
            {
                MusicLibrary = musiclibrary;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musiclibrary = await _context.MusicLibraries.FindAsync(id);
            if (musiclibrary != null)
            {
                MusicLibrary = musiclibrary;
                _context.MusicLibraries.Remove(MusicLibrary);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
