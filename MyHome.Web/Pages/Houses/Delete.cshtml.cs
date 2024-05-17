using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using MyHome.Web.Data;

using System.ComponentModel;

namespace MyHome.Web.Pages.Houses
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public class DeleteViewModel
        {
            public string? Id { get; set; }

            [DisplayName("Straße")]
            public string? Street { get; set; }

            [DisplayName("Hausnummer")]
            public string? Number { get; set; }

            [DisplayName("PLZ")]
            public string? PostalCode { get; set; }

            [DisplayName("Stadt")]
            public string? City { get; set; }
        }

        private readonly ILogger<DeleteModel> _logger;
        private readonly ApplicationDbContext _dbContext;

        public DeleteViewModel ViewModel { get; set; }

        public DeleteModel(ILogger<DeleteModel> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGet(string id)
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

            var user = await _dbContext.Users.FindAsync(userId);

            if (user is null)
            {
                return NotFound("Die Anmeldung ist ungültig!");
            }

            var house = await _dbContext.Houses.FindAsync(id);

            if (house == null)
            {
                return NotFound();
            }

            if (house.FamilyId != user.FamilyId)
            {
                return BadRequest("Sie sind nicht berechtigt für dieses Haus");
            }

            ViewModel = new DeleteViewModel
            {
                Street = house.Street,
                City = house.City,
                Number = house.Number,
                PostalCode = house.PostalCode,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return await OnGet(id);
            }

            var house = await _dbContext.Houses.FindAsync(id);

            if (house != null)
            {
                _dbContext.Houses.Remove(house);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("House {House} deleted by {User}", id, User.GetId());
            }

            return Redirect("/");
        }
    }
}
