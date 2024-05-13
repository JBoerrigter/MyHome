using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyHome.Web.Pages
{
    public class SettingsModel : PageModel
    {
        public bool IsDarkMode { get; set; }

        public IActionResult OnGet()
        {
            if (Request.Cookies.TryGetValue("darkmode", out string darkModeValue))
            {
                IsDarkMode = bool.TryParse(darkModeValue, out bool result) && result;
            }
            else
            {
                IsDarkMode = false;
            }
            return Page();
        }

        public IActionResult OnPost(bool darkmode)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Append("darkmode", darkmode.ToString(), options);
            return RedirectToPage();
        }
    }
}
