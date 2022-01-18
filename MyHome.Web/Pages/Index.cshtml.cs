
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

using System.Security.Claims;

namespace MyHome
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public int MeterReadingsConut = 0;
        public int CostsCount = 0;
        
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();
            }

            MeterReadingsConut = await _context.MetersReadings
                .Where(r => r.UserId == userId.Value)
                .CountAsync();

            CostsCount = await _context.Expenses
                .Where(c => c.UserId == userId.Value)
                .CountAsync();

            return Page();
        }
    }
}