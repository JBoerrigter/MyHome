
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

        public IList<Cost> Cost { get; set; }
        public string Error { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                
                if (userId == null)
                {
                    return Unauthorized();
                }

                Cost = await _context.Costs
                    .Where(c => c.UserId == userId.Value)
                    .Include(c => c.CostType)
                    .OrderByDescending(c => c.Year)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return Page();
        }
    }
}