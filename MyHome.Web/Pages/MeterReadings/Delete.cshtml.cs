﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyHome.Web.Data;

namespace MyHome.Web.Pages.MeterReadings
{
    public class DeleteModel : PageModel
    {
        private readonly MyHome.Web.Data.ApplicationDbContext _context;

        [BindProperty] public MeterReading MeterReading { get; set; }
        public string Base64Image { get; set; }

        public DeleteModel(MyHome.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MeterReading = await _context.MetersReadings
                .Include(m => m.ReadingType)
                .Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);

            if (MeterReading == null)
            {
                return NotFound();
            }

            if (MeterReading.Image is not null)
            {
                string base64String = Convert.ToBase64String(MeterReading.Image, 0, MeterReading.Image.Length);
                Base64Image = "data:image/jpg;base64," + base64String;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MeterReading = await _context.MetersReadings.FindAsync(id);

            if (MeterReading != null)
            {
                _context.MetersReadings.Remove(MeterReading);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
