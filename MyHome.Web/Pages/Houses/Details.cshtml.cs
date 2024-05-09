using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

using System.ComponentModel;

namespace MyHome.Web.Pages.Houses
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        public class DetailsViewModel
        {
            [DisplayName("Straße")]
            public string? Street { get; set; }

            [DisplayName("Hausnummer")]
            public string? Number { get; set; }

            [DisplayName("PLZ")]
            public string? PostalCode { get; set; }

            [DisplayName("Stadt")]
            public string? City { get; set; }

            public List<MeterReading>? MeterReadings { get; set; }
        }

        private readonly ApplicationDbContext _DbContext;

        public DetailsViewModel ViewModel { get; set; }

        public DetailsModel(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<IActionResult> OnGet(string? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var userId = User.GetId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var user = await _DbContext.Users.FindAsync(userId);

            if (user is null)
            {
                return NotFound("Die Anmeldung ist ungültig!");
            }

            var house = await _DbContext.Houses
                .Include(h => h.MeterReadings)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (house == null)
            {
                return NotFound();
            }

            if (house.FamilyId != user.FamilyId)
            {
                return BadRequest("Sie sind nicht berechtigt f�r dieses Haus");
            }

            ViewModel = new DetailsViewModel
            {
                Street = house.Street,
                City = house.City,
                Number = house.Number,
                PostalCode = house.PostalCode,
                MeterReadings = house.MeterReadings
            };

            return Page();
        }
    }
}
