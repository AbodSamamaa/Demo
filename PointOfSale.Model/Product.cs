using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSale.Model
{
    public partial class Product
    {
        public int IdProduct { get; set; }
        public string? BarCode { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public int? IdCategory { get; set; }
        public int? IdTax { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public byte[]? Photo { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }

		[ForeignKey("IdCategory")]
		public virtual Category? IdCategoryNavigation { get; set; }
		[ForeignKey("IdTax")]
		public virtual Tax IdTaxNavigation { get; set; }
    }
}