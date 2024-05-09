#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Pages.Expenses
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public DetailsModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Expense = await _context.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Expense == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
