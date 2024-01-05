using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.User.Releases
{
    public class ReviewsModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public ReviewsModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public IList<Review> Review { get;set; } = default!;
        public Release Release { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Release = await _context.Releases
                .Include(r => r.Artist)
                .Include(r => r.Songs)
                .FirstOrDefaultAsync(m => m.ID == id);
            Review = await _context.Reviews
                .Include(r => r.Release)
                .Where(r => r.ReleaseID == id)
                .Include(r => r.User)
                .ToListAsync();
            return Page();
        }
    }
}
