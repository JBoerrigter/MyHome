using Microsoft.AspNetCore.Identity;

namespace MyHome.Web.Data
{
    public class Family
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public List<House> Houses { get; set; }
    }
}
