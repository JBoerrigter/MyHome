#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.MeterReadingTypes
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public IndexModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MeterReadingType> MeterReadingType { get;set; }

        public async Task OnGetAsync()
        {
            MeterReadingType = await _context.MetersReadingTypes.ToListAsync();
        }
    }
}
