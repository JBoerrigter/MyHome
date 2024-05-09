using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHome.Web.Data.Config
{
    public class ExpenseTypeConfig : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {
            builder.Property(p => p.Id).HasDefaultValue(Guid.NewGuid().ToString());
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
