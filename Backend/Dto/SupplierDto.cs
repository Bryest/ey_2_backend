namespace Backend.Dto
{
    public class SupplierDto
    {
        public string BusinessName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string PhysicalAddress { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public decimal AnnualBilling { get; set; }
    }
}
