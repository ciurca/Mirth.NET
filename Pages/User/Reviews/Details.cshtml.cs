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

namespace ProiectMPD.Pages.User.Reviews
{
    public class DetailsModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public DetailsModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Review Review { get; set; } = default!;
        public string userId { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(u => u.User)
                .Include(u => u.Release)
                .ThenInclude(r => r.Artist)
                .FirstOrDefaultAsync(m => m.ID == id);
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user's ID
            if (review == null)
            {
                return NotFound();
            }
            else
            {
                Review = review;
            }
            return Page();
        }
    }
}
