
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHome.Web;
using MyHome.Web.Data;

using System.Security.Claims;

namespace MyHome
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _Context;

        public string? Username { get; set; }
        public Family? Family { get; private set; }

        public IndexModel(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.GetId();

            if (userId is null)
            {
                return Redirect("/Account/Login");
            }

            var user = await _Context.Users
                .Include(u => u.Family)
                .Include(u => u.Family.Houses)
                .FirstOrDefaultAsync(u => u.Id == userId);

            Username = user?.UserName;
            Family = user?.Family;

            return Page();
        }
    }
}