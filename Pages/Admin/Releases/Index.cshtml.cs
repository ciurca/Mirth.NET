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
    public class IndexModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public IndexModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Release> Release { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Release = await _context.Releases
                .Include(a=>a.Artist)
                .ToListAsync();
        }
    }
}
