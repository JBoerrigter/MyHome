#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using MyHome.Web.Data;

namespace MyHome.Web.Areas.Admin.Pages.MeterReadingTypes
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        public CreateModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MeterReadingType MeterReadingType { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MetersReadingTypes.Add(MeterReadingType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
