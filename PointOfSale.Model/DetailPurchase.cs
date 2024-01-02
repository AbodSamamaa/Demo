using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Model
{
    public partial class DetailPurchase
    {
        [Key]
        public int IdDetailPurchase { get; set; }
        public int? IdPurchase { get; set; }
        public int? IdCategory { get; set; }
        public string? BrandCategory { get; set; }
        public string? DescriptionCategory { get; set; }
        public string? Category { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }

        [ForeignKey("IdPurchase")]
        public virtual Purchase? IdPurchaseNavigation { get; set; }
    }
}
