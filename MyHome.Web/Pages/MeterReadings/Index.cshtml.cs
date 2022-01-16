#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

using System.Security.Claims;

namespace MyHome.Web.Pages.MeterReadings
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public IList<MeterReading> MeterReadingList { get; set; }

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

            MeterReadingList = await _context.MetersReadings
                .Where(m => m.UserId == userId.Value)
                .Include(m => m.ReadingType)
                .Include(m => m.User)
                .ToListAsync();

            return Page();
        }
    }
}
