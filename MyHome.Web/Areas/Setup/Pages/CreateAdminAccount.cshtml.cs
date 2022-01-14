using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

namespace MyHome.Web.Areas.Setup.Pages
{
    public class CreateAdminAccountModel : PageModel
    {
        private readonly IUserStore<IdentityUser> _UserStore;
        private readonly IUserEmailStore<IdentityUser> _EmailStore;
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly SignInManager<IdentityUser> _SignInManager;

        /// <summary>
        /// Data bound to the UI
        /// </summary>
        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public CreateAdminAccountModel(
            IUserStore<IdentityUser> userStore,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        {
            _UserStore = userStore;
            _UserManager = userManager;
            _RoleManager = roleManager;
            _SignInManager = signInManager;

            if (_UserManager.SupportsUserEmail)
            {
                _EmailStore = (IUserEmailStore<IdentityUser>)_UserStore;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new IdentityUser(Input.Email);
            await _UserStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);

            if (_EmailStore is not null)
            {
                await _EmailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            }

            var result = await _UserManager.CreateAsync(user, Input.Password);

            var adminRole = new IdentityRole("Administrator");
            var standardRole = new IdentityRole("Standard");

            var adminRoleResult = await _RoleManager.CreateAsync(adminRole);
            var standardRoleResult = await _RoleManager.CreateAsync(standardRole);

            await _UserManager.AddToRoleAsync(user, adminRole.Name);

            await _SignInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect("/");
        }

    }
}
