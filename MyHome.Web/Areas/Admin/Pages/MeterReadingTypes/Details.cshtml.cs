#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.MeterReadingTypes
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DetailsModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MeterReadingType MeterReadingType { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MeterReadingType = await _dbContext.MetersReadingTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (MeterReadingType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
