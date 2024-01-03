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
    public class DeleteModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public DeleteModel(ProiectMPD.Data.ApplicationDbContext context)
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

            var release = await _context.Releases.FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Releases.FindAsync(id);
            if (release != null)
            {
                Release = release;
                _context.Releases.Remove(Release);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
