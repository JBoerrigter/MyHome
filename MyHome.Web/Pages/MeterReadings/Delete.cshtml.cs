#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Pages.MeterReadings
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        [BindProperty(SupportsGet = true)]
        public string HouseId { get; set; }

        [BindProperty] public MeterReading MeterReading { get; set; }
        public string Base64Image { get; set; }

        public DeleteModel(ILogger<DeleteModel> logger, ApplicationDbContext context)
        {
            this._logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string? id)
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

        public async Task<IActionResult> OnPostAsync(string? id)
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

                _logger.LogInformation("Reading {Meters Reading} deleted by {User}.", 
                    id, MeterReading.HouseId, MeterReading.Year, User.GetId());
            }

            return Redirect($"/Houses/{HouseId}");
        }
    }
}
