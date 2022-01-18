#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.ExpenseTypes
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public DetailsModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ExpenseType ExpenseType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExpenseType = await _context.ExpenseTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (ExpenseType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
