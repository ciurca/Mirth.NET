using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.Genres
{
    public class IndexModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public IndexModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Genre = await _context.Genres.ToListAsync();
        }
    }
}
