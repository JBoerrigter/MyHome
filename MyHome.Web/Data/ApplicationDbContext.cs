using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyHome.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Cost> Costs { get; set; }
        public DbSet<CostType> CostTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}