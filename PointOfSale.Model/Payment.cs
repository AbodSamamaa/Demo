using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Model
{
	public class Payment
	{
		[Key]
		public int? IdPayment { get; set; }
		public string? PaymentMethod { get; set; }
		public string? Description { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? RegistrationDate { get; set; }
	}
}
