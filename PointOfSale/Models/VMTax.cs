using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Models
{
	public class VMTax
	{
		[Key]
		public int IdTax { get; set; }
		public float? Percentage { get; set; }
		public string? Description { get; set; }
		public int? IsFixed { get; set; }
		public int? IsExternal { get; set; }
		public int? IsActive { get; set; }
		public DateTime? RegistrationDate { get; set; }
	}
}
