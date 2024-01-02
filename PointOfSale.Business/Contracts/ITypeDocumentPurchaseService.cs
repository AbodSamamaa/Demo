using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Business.Contracts
{
    public interface ITypeDocumentPurchaseService
    {
        Task<List<TypeDocumentPurchase>> List();
		Task<List<TypeDocumentPurchase>> ListForTypeDocumentPurchases();
		Task<TypeDocumentPurchase> Add(TypeDocumentPurchase entity);
        Task<TypeDocumentPurchase> Edit(TypeDocumentPurchase entity);
        Task<bool> Delete(int idTax);
    }
}
