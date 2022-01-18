#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.MeterReadingTypes
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public EditModel(MyHome.Web.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MeterReadingType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeterReadingTypeExists(MeterReadingType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MeterReadingTypeExists(int id)
        {
            return _context.MetersReadingTypes.Any(e => e.Id == id);
        }
    }
}
