using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Business.Contracts
{
    public interface IPurchaseService
    {

        Task<List<Category>> GetCategories(string search);

        Task<Purchase> Register(Purchase entity);

        Task<List<Purchase>> PurchaseHistory(string PurchaseNumber, string StarDate, string EndDate);
        Task<Purchase> Detail(string PurchaseNumber);
        Task<List<DetailPurchase>> Report(string StarDate, string EndDate);
    }
}
