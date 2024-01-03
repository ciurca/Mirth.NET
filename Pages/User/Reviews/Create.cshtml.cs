using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.User.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public CreateModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ReleaseID"] = new SelectList(_context.Releases, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Review Review { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user's ID
            if (_context.Reviews.Any(r => r.ReleaseID == Review.ReleaseID && r.UserId == userId))
            {
                TempData["ErrorMessage"] = "You already reviewed this release.";
                return RedirectToPage("./Index");
            }
            Review.UserId = userId;
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
