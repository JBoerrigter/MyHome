using System.ComponentModel.DataAnnotations;

namespace MyHome.Web.ViewModels
{
    public class FamilyViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
