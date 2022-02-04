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
            public int Id { get; set; }

            [DisplayName("Straße")]
            public string Street { get; set; }

            [DisplayName("Hausnummer")]
            public string Number { get; set; }

            [DisplayName("PLZ")]
            public string PostalCode { get; set; }

            [DisplayName("Stadt")]
            public string City { get; set; }
        }

        private readonly ApplicationDbContext _DbContext;

        public DeleteViewModel ViewModel { get; set; }

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<IActionResult> OnGet(int? id)
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

            var house = await _DbContext.Houses.FindAsync(id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return await OnGet(id);
            }

            var house = await _DbContext.Houses.FindAsync(id);

            if (house != null)
            {
                _DbContext.Houses.Remove(house);
                await _DbContext.SaveChangesAsync();
            }

            return Redirect("/");
        }
    }
}
