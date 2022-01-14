#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

namespace MyHome.Pages.Costs
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cost Cost { get; set; }

        public string Base64Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cost = await _context.Costs
                .Include(c => c.CostType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Cost == null)
            {
                return NotFound();
            }

            string base64String = Convert.ToBase64String(Cost.Image, 0, Cost.Image.Length);
            Base64Image = "data:image/jpg;base64," + base64String;

            return Page();
        }
    }
}
