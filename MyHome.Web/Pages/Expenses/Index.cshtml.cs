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
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Expense> Expense { get;set; }

        public async Task OnGetAsync()
        {
            Expense = await _dbContext.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.User).ToListAsync();
        }
    }
}
