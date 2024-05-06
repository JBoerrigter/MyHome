using Microsoft.AspNetCore.Identity;

namespace MyHome.Data
{
    public class MeterReading
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public DateTime Created { get; set; }

        public Guid ReadingTypeId { get; set; }
        public MeterReadingType? ReadingType { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public Guid HouseId { get; set; }
        public House? House { get; set; }

    }
}
