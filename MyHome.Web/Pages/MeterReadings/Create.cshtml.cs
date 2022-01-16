#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using MyHome.Web.Data;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MyHome.Web.Pages.MeterReadings
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public InputModel Input { get; set; }

        public string Error { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Art")]
            public int ReadingTypeId { get; set; }

            [Required]
            [Display(Name = "Jahr")]
            public int Year { get; set; }

            [Required]
            [Display(Name = "Stand")]
            public int Value { get; set; }

            [MaxLength(250)]
            [Display(Name = "Bemerkung")]
            public string? Description { get; set; }

            [Display(Name = "Bild / Screenshot")]
            public IFormFile? Image { get; set; }
        }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Types"] = new SelectList(_context.MetersReadingTypes, nameof(MeterReadingType.Id), nameof(MeterReadingType.Name));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    if (userId is null)
                    {
                        return Unauthorized();
                    }

                    var reading = new MeterReading
                    {
                        ReadingTypeId = Input.ReadingTypeId,
                        Year = Input.Year,
                        Value = Input.Value,
                        Description = Input.Description,
                        Created = DateTime.Now,
                        UserId = userId.Value
                    };

                    if (Input.Image is not null)
                    {
                        var buffer = new byte[Input.Image.Length];
                        using (var stream = new MemoryStream(buffer))
                        {
                            await Input.Image.CopyToAsync(stream);
                        }
                        reading.Image = buffer;
                    }

                    _context.MetersReadings.Add(reading);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return OnGet();
        }
    }
}
