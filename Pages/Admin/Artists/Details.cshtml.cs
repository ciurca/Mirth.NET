using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.Artists
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public DetailsModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Artist Artist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FirstOrDefaultAsync(m => m.ID == id);
            if (artist == null)
            {
                return NotFound();
            }
            else
            {
                Artist = artist;
            }
            return Page();
        }
    }
}
