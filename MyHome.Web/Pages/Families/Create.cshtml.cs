using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using MyHome.Web.Data;

using System.Security.Claims;

namespace MyHome.Web.Pages.Families
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty] public string? Name { get; set; } 

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

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound("Die Anmeldung ist ungültig!");
            }

            var family = new Family
            {
                Name = Name,
                Members = new List<ApplicationUser>()
            };
            family.Members.Add(user);

            var result = await _dbContext.Families.AddAsync(family);
            
            user.Family = result.Entity;

            await _dbContext.SaveChangesAsync();

            return Redirect("/");
        }
    }
}
