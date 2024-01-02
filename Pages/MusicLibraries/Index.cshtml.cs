using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;

namespace ProiectMPD.Pages.MusicLibraries
{
    public class IndexModel : PageModel
    {
        private readonly ProiectMPD.Data.ApplicationDbContext _context;

        public IndexModel(ProiectMPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MusicLibrary> MusicLibrary { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MusicLibrary = await _context.MusicLibraries
                .Include(m => m.User).ToListAsync();
        }
    }
}
