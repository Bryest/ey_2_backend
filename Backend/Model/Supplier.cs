﻿namespace Backend.Model
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
        public string PhoneNumber{ get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string PhysicalAddress { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public decimal AnnualBilling { get; set; } 
        public DateTime LastEdited { get; set; }
    }
}
