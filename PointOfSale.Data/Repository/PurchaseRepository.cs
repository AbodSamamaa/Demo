using Microsoft.EntityFrameworkCore;
using PointOfSale.Data.DBContext;
using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Data.Repository
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        private readonly POINTOFSALEContext _dbcontext;
        public PurchaseRepository(POINTOFSALEContext context) : base(context)
        {
            _dbcontext = context;
        }
        public async Task<Purchase> Register(Purchase entity)
        {

            Purchase PurchaseGenerated = new Purchase();
            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetailPurchase dv in entity.DetailPurchases)
                    {
                        dv.IdCategory = entity.IdCategory;
                        Category Category_found = _dbcontext.Categories.Where(p => p.IdCategory == dv.IdCategory).First();

                        Category_found.IdCategory = Category_found.IdCategory;
                        _dbcontext.Categories.Update(Category_found);
                    }
                    await _dbcontext.SaveChangesAsync();

                    CorrelativeNumber correlative = _dbcontext.CorrelativeNumbers.Where(n => n.Management == "Purchase").First();
                    if (correlative != null)
                    {
                        correlative.LastNumber = correlative.LastNumber + 1;
                        correlative.DateUpdate = DateTime.Now;

                        _dbcontext.CorrelativeNumbers.Update(correlative);
                        await _dbcontext.SaveChangesAsync();
                   

                    string ceros = string.Concat(Enumerable.Repeat("0", correlative.QuantityDigits.Value));
                    string PurchaseNumber = ceros + correlative.LastNumber.ToString();
                    PurchaseNumber = PurchaseNumber.Substring(PurchaseNumber.Length - correlative.QuantityDigits.Value, correlative.QuantityDigits.Value);

                    entity.PurchaseNumber = PurchaseNumber;

                    await _dbcontext.Purchases.AddAsync(entity);
                    await _dbcontext.SaveChangesAsync();

                    PurchaseGenerated = entity;

                    transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return PurchaseGenerated;
        }
        public async Task<List<DetailPurchase>> Report(DateTime StarDate, DateTime EndDate)
        {
            List<DetailPurchase> listSummary = await _dbcontext.DetailPurchases
                .Include(v => v.IdPurchaseNavigation)
                .ThenInclude(u => u.IdUsersNavigation)
                .Include(v => v.IdPurchaseNavigation)
                .ThenInclude(tdv => tdv.IdTypeDocumentNavigation)
                .Where(dv => dv.IdPurchaseNavigation.RegistrationDate.Value.Date >= StarDate.Date && dv.IdPurchaseNavigation.RegistrationDate.Value.Date <= EndDate.Date)
                .ToListAsync();

            return listSummary;
        }
    }
}
