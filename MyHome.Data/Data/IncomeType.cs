namespace MyHome.Data
{
    public enum IncomeInterval
    {
        Daily = 0,
        Weekly = 1,
        Monthly = 2,
        Yearly = 3,
    }

    public class IncomeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IncomeInterval Interval { get; set; }
    }
}
