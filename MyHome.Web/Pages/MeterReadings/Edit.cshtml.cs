#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Pages.MeterReadings
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty(SupportsGet = true)]
        public string HouseId { get; set; }

        [BindProperty]
        public MeterReading MeterReading { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MeterReading = await _dbContext.MetersReadings
                .Include(m => m.ReadingType)
                .Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);

            if (MeterReading == null)
            {
                return NotFound();
            }
           ViewData["ReadingTypeId"] = new SelectList(_dbContext.MetersReadingTypes, "Id", "Id");
           ViewData["UserId"] = new SelectList(_dbContext.Users, "Id", "Id");
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

            _dbContext.Attach(MeterReading).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeterReadingExists(MeterReading.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect($"/Houses/{HouseId}");
        }

        private bool MeterReadingExists(string id)
        {
            return _dbContext.MetersReadings.Any(e => e.Id == id);
        }
    }
}
