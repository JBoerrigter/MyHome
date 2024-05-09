namespace MyHome.Web.Data
{
    public class House
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool Rented { get; set; }
        
        public string FamilyId { get; set; }
        public Family? Family { get; set; }

        public List<MeterReading> MeterReadings { get; set; }
    }
}
