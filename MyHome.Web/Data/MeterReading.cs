using Microsoft.AspNetCore.Identity;

namespace MyHome.Web.Data
{
    public class MeterReading
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Year { get; set; }
        public double Value { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public DateTime Created { get; set; }

        public string ReadingTypeId { get; set; }
        public MeterReadingType? ReadingType { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string HouseId { get; set; }
        public House House { get; set; }

    }
}
