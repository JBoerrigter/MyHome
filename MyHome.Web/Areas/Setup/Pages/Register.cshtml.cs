using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHome.Web.Data;

using System.ComponentModel.DataAnnotations;

namespace MyHome.Web.Areas.Setup.Pages
{
    public class Register : PageModel
    {
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser>? _emailStore;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// Data bound to the UI
        /// </summary>
        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string? Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string? Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string? ConfirmPassword { get; set; }
        }

        public Register(
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userStore = userStore;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

            if (_userManager.SupportsUserEmail)
            {
                _emailStore = (IUserEmailStore<ApplicationUser>)_userStore;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (await _userManager.Users.AnyAsync())
            {
                return RedirectPermanent("/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new ApplicationUser(Input.Email!);
            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);

            if (_emailStore is not null)
            {
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            }

            var result = await _userManager.CreateAsync(user, Input.Password!);

            var adminRole = new IdentityRole("Administrator");
            var standardRole = new IdentityRole("Standard");

            var adminRoleResult = await _roleManager.CreateAsync(adminRole);
            var standardRoleResult = await _roleManager.CreateAsync(standardRole);

            await _userManager.AddToRoleAsync(user, adminRole.Name!);

            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect("/");
        }

    }
}
