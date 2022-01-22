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
        private readonly ApplicationDbContext _DbContext;

        [BindProperty] public string Name { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
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

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _DbContext.Users.FindAsync(userId.Value);

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

            var result = await _DbContext.Families.AddAsync(family);
            
            user.Family = result.Entity;

            await _DbContext.SaveChangesAsync();

            return Redirect("/");
        }
    }
}
