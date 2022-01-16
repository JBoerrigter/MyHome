using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

using System.Security.Claims;

namespace MyHome.Web.Pages.Costs
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _DbContext;

        public IEnumerable<Cost>? CostList { get; set; }

        public IndexModel(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();
            }

            CostList = await _DbContext.Costs
                .Where(c => c.UserId == userId.Value)
                .ToListAsync();

            return Page();
        }
    }
}
