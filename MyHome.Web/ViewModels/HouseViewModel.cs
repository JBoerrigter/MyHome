using System.ComponentModel.DataAnnotations;

namespace MyHome.Web.ViewModels
{
    public class HouseViewModel
    {
        [Required]
        [MaxLength(200)]
        public string? Street { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Number {  get; set; }

        [Required]
        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string? City { get; set; }
    }
}
