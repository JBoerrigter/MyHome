using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHome.Data.Config
{
    public class HouseConfig : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.Property(p => p.FamilyId).IsRequired();
            builder.Property(p => p.Street).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Number).IsRequired().HasMaxLength(10);
            builder.Property(p => p.City).IsRequired().HasMaxLength(200);
            builder.Property(p => p.PostalCode).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Rented).HasDefaultValue(false);
        }
    }
}
