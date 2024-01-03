using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.Releases
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public DetailsModel(ProiectMPD.Data.ApplicationDbContext context)
        {
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
            return Page();
        }
    }
}
