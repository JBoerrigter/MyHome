#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Pages.Expenses
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public IndexModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get;set; }

        public async Task OnGetAsync()
        {
            Expense = await _context.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.User).ToListAsync();
        }
    }
}
