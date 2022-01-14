#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MyHome.Web.Data;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MyHome.Pages.Costs
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string Error { get; private set; }
        public SelectList CostTypes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BindProperty]
        [Required]
        public int Year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BindProperty]
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Der Wert {0} muss größer als {1} sein.")]
        public double Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BindProperty]
        [Required]
        public int CostTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BindProperty]
        public string Description { get; set; }

        public IFormFile Image { get; set; }
        
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                    return Unauthorized();

                var types = await _context.CostTypes.ToListAsync();
                types = types.OrderBy(t => t.Name).ToList();
                CostTypes = new SelectList(types, nameof(CostType.Id), nameof(CostType.Name));
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                return await OnGet();
            }

            if (Year < 1970 || Year > DateTime.Now.Year)
            {
                ModelState.AddModelError(nameof(Year), "Bitte geben Sie ein gültiges Jahr an.");
                return await OnGet();
            }

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();
            }

            byte[] buffer = new byte[Image.Length];
            using (var stream = new MemoryStream(buffer))
            {
                await Image.CopyToAsync(stream);
            }

            var cost = new Cost
            {
                Year = Year,
                Value = Value,
                CostTypeId = CostTypeId,
                UserId = userId.Value,
                Created = DateTime.Now,
                Description = Description,
                Image = buffer
            };

            _context.Costs.Add(cost);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
