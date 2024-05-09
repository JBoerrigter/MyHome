using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyHome.Web.Data.Config
{
    public class MeterReadingConfig : IEntityTypeConfiguration<MeterReading>
    {
        public void Configure(EntityTypeBuilder<MeterReading> builder)
        {
            builder.Property(p => p.Id).HasDefaultValue(Guid.NewGuid().ToString());
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.ReadingTypeId).IsRequired();
            builder.Property(p => p.Year).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Created).ValueGeneratedOnAdd();
        }
    }
}
