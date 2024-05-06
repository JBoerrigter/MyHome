using Microsoft.AspNetCore.Identity;

namespace MyHome.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser() { }
        public ApplicationUser(string userName) : base(userName) { }

        public Guid? FamilyId { get; set; }
        public Family? Family { get; set; }
    }
}
