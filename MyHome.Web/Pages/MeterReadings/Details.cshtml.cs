#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Pages.MeterReadings
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public MeterReading MeterReading { get; set; }

        public string Base64Image { get; set; }


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

            if (MeterReading.Image is not null)
            {
                string base64String = Convert.ToBase64String(MeterReading.Image, 0, MeterReading.Image.Length);
                Base64Image = "data:image/jpg;base64," + base64String;
            }

            return Page();
        }
    }
}
