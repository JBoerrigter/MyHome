#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using MyHome.Web.Data;

namespace MyHome.Web.Pages.Expenses
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
        ViewData["ExpenseTypeId"] = new SelectList(_dbContext.ExpenseTypes, "Id", "Id");
        ViewData["UserId"] = new SelectList(_dbContext.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Expenses.Add(Expense);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
