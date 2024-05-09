using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MyHome.Web.Pages.Houses
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public class InputModel
        {
            [Required]
            [MaxLength(200)]
            public string? Street { get; set; }
            
            [Required]
            [MaxLength(10)]
            public string? Number { get; set; }
            
            [Required] 
            [MaxLength(10)]
            public string? PostalCode { get; set; }
            
            [Required] 
            [MaxLength(200)]
            public string? City { get; set; }
        }

        [BindProperty] 
        public InputModel Input { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            if (string.IsNullOrEmpty(user.FamilyId))
            {
                return BadRequest("Sie haben noch keine Familie. Bitte erstellen Sie zuerst eine Familie!");
            }

            var house = new House
            {
                Street = Input.Street,
                Number = Input.Number,
                PostalCode = Input.PostalCode,
                City = Input.City,
                FamilyId = user.FamilyId
            };

            await _dbContext.Houses.AddAsync(house);
            await _dbContext.SaveChangesAsync();

            return Redirect("/");
        }
    }
}
