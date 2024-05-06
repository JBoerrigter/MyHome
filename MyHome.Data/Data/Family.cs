namespace MyHome.Data;

public class Family
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ApplicationUser>? Members { get; set; }
        public List<House>? Houses { get; set; }
    }
