using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.User.Reviews
{
    public class DeleteModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public DeleteModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FirstOrDefaultAsync(m => m.ID == id);

            if (review == null)
            {
                return NotFound();
            }
            else
            {
                Review = review;
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Review.UserId!= userId)
            {
                TempData["ErrorMessage"] = "Ooops. Couldn't get that for you.";
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                Review = review;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
                if (Review.UserId!= userId)
                {
                    TempData["ErrorMessage"] = "Ooops. Couldn't get that for you.";
                    return RedirectToPage("./Index");
                }
                _context.Reviews.Remove(Review);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
