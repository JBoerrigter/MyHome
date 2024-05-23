using System.ComponentModel.DataAnnotations;

namespace MyHome.Web.ViewModels
{
    public class MeterReadingTypeViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Unit {  get; set; }
    }
}
