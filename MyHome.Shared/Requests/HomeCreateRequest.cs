using System;

namespace MyHome.Shared.Requests
{
    public class HomeCreateRequest
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}

