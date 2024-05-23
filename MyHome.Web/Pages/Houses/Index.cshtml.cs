using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHome.Web.Data;

namespace MyHome.Web.Pages.Houses
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Family? Family { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.GetId();

            if (userId is null)
            {
                return Redirect("/Account/Login");
            }

            var user = await _context.Users
                .Include(u => u.Family)
                .Include(u => u.Family.Houses)
                .FirstOrDefaultAsync(u => u.Id == userId);

            Family = user?.Family;

            return Page();
        }
    }
}
