using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.MusicLibraries
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
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public MusicLibrary MusicLibrary { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MusicLibraries.Add(MusicLibrary);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
