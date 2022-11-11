using Microsoft.AspNetCore.Identity;

namespace MyHome.Data
{
    public class Income
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime Created { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser? User { get; set; }

        public int IncomeTypeId { get; set; }
        public IncomeType? IncomeType { get; set; }
    }
}
