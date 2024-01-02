using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
	public class VMPayment
	{
		[Key]
		public int? IdPayment { get; set; }
		public string? PaymentMethod { get; set; }
		public string? Description { get; set; }
		public bool? IsActive { get; set; }
	}
}
