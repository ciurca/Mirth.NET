using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.Songs
{
    public class IndexModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public IndexModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Song> Song { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Song = await _context.Songs
                .Include(s => s.Release).ToListAsync();
        }
    }
}
