using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ProiectMPD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string userId { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToPage("/Admin/Releases/Index");
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToPage("/User/Library/Index");
            }
            else
            {
                return Page(); 
            }
        }

    }
}
