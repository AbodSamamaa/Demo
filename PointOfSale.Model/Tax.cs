using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Model
{
	public class Tax
	{
		[Key]
		public int IdTax { get; set; }
		public float? Percentage { get; set; }
        public string? Description { get; set; }
        public bool? IsFixed { get; set; }
        public bool? IsExternal { get; set; }
        public bool? IsActive { get; set; }
		public DateTime? RegistrationDate { get; set; }

		public virtual ICollection<Product> Products { get; set; }
	}
}
