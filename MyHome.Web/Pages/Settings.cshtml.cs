using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHome.Web.Pages
{
    public class SettingsModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
