using System;

namespace MyHome.Shared
{
    public class HomeViewModel
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}