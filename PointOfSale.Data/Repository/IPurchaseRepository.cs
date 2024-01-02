using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Data.Repository
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<Purchase> Register(Purchase entity);

        Task<List<DetailPurchase>> Report(DateTime StartDate, DateTime EndDate);

    }
}