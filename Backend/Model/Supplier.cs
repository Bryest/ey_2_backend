namespace Backend.Model
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string BussinessName { get; set; }
        public string TradeName { get; set; }
        public string TaxId { get; set; }
        public string PhoneNumber{ get; set; }
        public string Email { get; set; }
        public string Website{ get; set; }
        public string PhysicalAddress { get; set; }
        public string Country{ get; set; }
        public decimal AnnualBilling { get; set; }
        public DateTime LastEdited{ get; set; }
    }
}
