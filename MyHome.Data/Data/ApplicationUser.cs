using Microsoft.AspNetCore.Identity;

namespace MyHome.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }
        public ApplicationUser(string userName) : base(userName) { }

        public int? FamilyId { get; set; }
        public Family? Family { get; set; }
    }
}
