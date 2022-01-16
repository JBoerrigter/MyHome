using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Setup.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly ApplicationDbContext _DbContext;

        public bool IsDatabaseCreated
        {
            get { return !_DbContext.Database.GetPendingMigrations().Any(); }
        }

        public IndexModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _RoleManager = roleManager;
            _DbContext = dbContext;
        }

        public async Task<IActionResult> OnGet()
        {
            if (IsDatabaseCreated)
            {
                if (await _RoleManager.RoleExistsAsync("Administrator"))
                {
                    return BadRequest("Setup has already been completed");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnGetCreateDatabaseAsync()
        {
            EnsureDataPathExists();
            await _DbContext.Database.MigrateAsync(CancellationToken.None);
            return Page();
        }

        /// <summary>
        /// Todo: more generic
        /// </summary>
        /// <remarks>
        /// just a workaround for the time beeing 
        /// </remarks>
        private static void EnsureDataPathExists()
        {
            DirectoryInfo directory = new("data");
         
            if (!directory.Exists)
            {
                directory.Create();
            }
        }
    }
}
