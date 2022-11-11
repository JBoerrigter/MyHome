using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyHome.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Family> Families { get; set; } = null!;
        public DbSet<House> Houses { get; set; } = null!;

        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<IncomeType> IncomeTypes { get; set; } = null!;

        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<ExpenseType> ExpenseTypes { get; set; } = null!;

        public DbSet<MeterReading> MetersReadings { get; set; } = null!;
        public DbSet<MeterReadingType> MetersReadingTypes { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}