namespace MyHome.Web.Data
{
    public class House
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool Rented { get; set; }
        
        public int FamilyId { get; set; }
        public Family? Family { get; set; }
    }
}
