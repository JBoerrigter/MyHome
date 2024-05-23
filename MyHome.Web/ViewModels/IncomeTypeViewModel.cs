using System.ComponentModel.DataAnnotations;

namespace MyHome.Web.ViewModels
{
    public class IncomeTypeViewModel
    {
        [Required]
        public string? Name {  get; set; }
    }
}
