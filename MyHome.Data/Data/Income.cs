namespace MyHome.Data
{
    public class Income
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime Created { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public Guid IncomeTypeId { get; set; }
        public IncomeType? IncomeType { get; set; }
    }
}
