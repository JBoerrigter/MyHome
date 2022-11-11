using Microsoft.AspNetCore.Identity;

namespace MyHome.Data
{
    public class Expense
    {
        public int Id { get; set; }
        public string Descrition { get; set; } = null!;
        public double Value { get; set; }
        
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser? User { get; set; }

        public int ExpenseTypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }
    }
}
