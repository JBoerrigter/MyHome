using Microsoft.AspNetCore.Identity;

namespace MyHome.Web.Data
{
    public class Cost
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int Year { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public IdentityUser? User { get; set; }
        public int CostTypeId { get; set; }
        public CostType? CostType { get; set; }
    }
}
