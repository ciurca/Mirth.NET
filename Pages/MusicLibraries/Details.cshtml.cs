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
    public class DetailsModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public DetailsModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
