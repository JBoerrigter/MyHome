namespace MyHome.Web.Data
{
    public class ExpenseType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}
