namespace MyHome.Web.Data
{
    public class MeterReadingType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
