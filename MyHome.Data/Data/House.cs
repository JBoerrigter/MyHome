﻿namespace MyHome.Data
{
    public class House
    {
        public Guid Id { get; set; }
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public bool Rented { get; set; }
        
        public Guid FamilyId { get; set; }
        public Family? Family { get; set; }

        public List<MeterReading> MeterReadings { get; set; } = null!;
    }
}
