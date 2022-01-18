#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.IncomeTypes
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public IndexModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<IncomeType> IncomeType { get;set; }

        public async Task OnGetAsync()
        {
            IncomeType = await _context.IncomeTypes.ToListAsync();
        }
    }
}
