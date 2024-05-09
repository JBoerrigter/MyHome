namespace MyHome.Web.Data
{
    public class Expense
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Descrition { get; set; }
        public double Value { get; set; }
        
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string? ExpenseTypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }
    }
}
