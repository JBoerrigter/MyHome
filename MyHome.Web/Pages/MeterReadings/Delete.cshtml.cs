#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHome.Web.Data;

namespace MyHome.Web.Pages.MeterReadings
{
    public class DeleteModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public DeleteModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MeterReading MeterReading { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MeterReading = await _context.MetersReadings
                .Include(m => m.ReadingType)
                .Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);

            if (MeterReading == null)
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

            MeterReading = await _context.MetersReadings.FindAsync(id);

            if (MeterReading != null)
            {
                _context.MetersReadings.Remove(MeterReading);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
