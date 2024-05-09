#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.IncomeTypes
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public DetailsModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IncomeType IncomeType { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IncomeType = await _dbContext.IncomeTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (IncomeType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
