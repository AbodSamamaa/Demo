using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Model
{
    public partial class TypeDocumentPurchase
    {
        public TypeDocumentPurchase()
        {
            Purchases = new HashSet<Purchase>();
        }
        [Key]
        public int IdTypeDocumentPurchase { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
