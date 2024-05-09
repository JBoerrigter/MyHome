using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHome.Web.Data.Config
{
    public class MeterReadingTypeConfig : IEntityTypeConfiguration<MeterReadingType>
    {
        public void Configure(EntityTypeBuilder<MeterReadingType> builder)
        {
            builder.Property(p => p.Id).HasDefaultValue(Guid.NewGuid().ToString());
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Unit).IsRequired().HasMaxLength(10);
        }
    }
}
