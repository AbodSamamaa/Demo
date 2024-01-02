using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Model
{
    public class Purchase
    {
        public Purchase()
        {
            DetailPurchases = new HashSet<DetailPurchase>();
        }

        [Key]
        public int IdPurchase { get; set; }
        public string? BarCode { get; set; }
        public string? Description { get; set; }
        public string? PurchaseNumber { get; set; }
        public string? Title { get; set; }
        public int? IdTypeDocumentPurchase { get; set; }
        public int? IdCategory { get; set; }
        public int? IdUsers { get; set; }
        public int? IdPaymentMethod { get; set; }
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

        [ForeignKey("IdTypeDocumentPurchase")]
        public virtual TypeDocumentPurchase? IdTypeDocumentNavigation { get; set; }
        [ForeignKey("IdPaymentMethod")]
        public virtual Payment? IdPaymentMethodNavigation { get; set; }
        [ForeignKey("IdUsers")]
        public virtual User? IdUsersNavigation { get; set; }
        [ForeignKey("IdCategory")]
        public virtual Category? IdCategoryNavigation { get; set; }

        public virtual ICollection<DetailPurchase> DetailPurchases { get; set; }
    }
}
