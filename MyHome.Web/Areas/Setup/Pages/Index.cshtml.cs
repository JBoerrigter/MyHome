using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace MyHome.Web.Areas.Setup.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _RoleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _RoleManager = roleManager;
        }

        public async Task<IActionResult> OnGet()
        {
            if (await _RoleManager.RoleExistsAsync("Administrator"))
            {
                return BadRequest("Setup has already been completed");
            }

            return Page();
        }
    }
}
