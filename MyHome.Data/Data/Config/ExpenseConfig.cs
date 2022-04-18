using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHome.Data.Config
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.ExpenseTypeId).IsRequired();
            builder.Property(p => p.Descrition).HasMaxLength(200);
            builder.Property(p => p.Created).ValueGeneratedOnAdd();
        }
    }
}
