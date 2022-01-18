using Microsoft.AspNetCore.Identity;

namespace MyHome.Web.Data
{
    public class Income
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int IncomeTypeId { get; set; }
        public IncomeType? IncomeType { get; set; }
    }
}
