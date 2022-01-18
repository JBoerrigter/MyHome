#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.MeterReadingTypes
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public DeleteModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MeterReadingType MeterReadingType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MeterReadingType = await _context.MetersReadingTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (MeterReadingType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MeterReadingType = await _context.MetersReadingTypes.FindAsync(id);

            if (MeterReadingType != null)
            {
                _context.MetersReadingTypes.Remove(MeterReadingType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
