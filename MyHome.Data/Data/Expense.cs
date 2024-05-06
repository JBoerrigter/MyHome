namespace MyHome.Data
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string Descrition { get; set; } = null!;
        public double Value { get; set; }
        
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public Guid ExpenseTypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }
    }
}
