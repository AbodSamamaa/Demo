namespace PointOfSale.Models
{
    public class VMPurchasesReport
    {
        public string? RegistrationDate { get; set; }
        public string? PurchaseNumber { get; set; }
        public string? DocumentType { get; set; }
        public string? SellerDocument { get; set; }
        public string? SellerName { get; set; }
        public string? Subtotal { get; set; }
        public string? TotalTaxes { get; set; }
        public string? Discount { get; set; }
        public string? UnitPrice { get; set; }
        public string? Category { get; set; }
        public string? Total { get; set; }
        public string? FinalPrice { get; set; }
        public string? Quantity { get; set; }
        public string? PaymentMethod { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Notes { get; set; }
    }
}
