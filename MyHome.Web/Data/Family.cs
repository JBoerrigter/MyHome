namespace MyHome.Web.Data
{
    public class Family
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public List<ApplicationUser>? Members { get; set; }
        public List<House>? Houses { get; set; }
    }
}
