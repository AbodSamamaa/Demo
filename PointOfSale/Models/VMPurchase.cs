using PointOfSale.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSale.Models
{
    public class VMPurchase
    {
        public int IdPurchase { get; set; }
        public string? BarCode { get; set; }
        public string? Description { get; set; }
        public string? PurchaseNumber { get; set; }
        public int? IdTypeDocumentPurchase { get; set; }
        public int? IdPaymentMethod { get; set; }
        public int? IdUsers { get; set; }
        public int? IdCategory { get; set; }
        public string? Title { get; set; }
        public string? SellerDocument { get; set; }
        public string? SellerName { get; set; }
        public string? PhoneNo { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? TotalTaxes { get; set; }
        public decimal? Discount { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Total { get; set; }
        public int? Quantity { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual ICollection<VMDetailPurchase> DetailPurchases { get; set; }
  
    }
}
