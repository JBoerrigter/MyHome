using System.ComponentModel.DataAnnotations;

namespace MyHome.Web.ViewModels
{
    public class MeterReadingViewModel
    {
        [Required]
        [Range(2000, 9999)]
        public int Year { get; set; } = DateTime.Now.Year;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public double Value { get; set; }

        [Required(ErrorMessage = "The type is required")]
        public string? MeterReadingTypeId { get; set; }

        [Required]
        public string? HouseId { get; set; }

        [Required]
        public string? UserId { get; set; }

        public string? Description { get; set; }
        public byte[]? Image { get; set; }
    }
}
