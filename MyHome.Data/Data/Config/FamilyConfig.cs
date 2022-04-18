using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHome.Data.Config
{
    public class FamilyConfig : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.HasMany(p => p.Houses);
            builder.HasMany(p => p.Members).WithOne(p => p.Family);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        }
    }
}
