namespace MyHome.Data
{
    public class MeterReadingType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!;
    }
}
