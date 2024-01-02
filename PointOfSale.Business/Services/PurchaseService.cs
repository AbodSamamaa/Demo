using Microsoft.EntityFrameworkCore;
using PointOfSale.Business.Contracts;
using PointOfSale.Data.Repository;
using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Business.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IGenericRepository<Category> _repositoryCategory;
        private readonly IPurchaseRepository _repositoryPurchase;
        public PurchaseService(IGenericRepository<Category> repositoryCategory, IPurchaseRepository repositoryPurchase)
        {
            _repositoryCategory = repositoryCategory;
            _repositoryPurchase = repositoryPurchase;
        }
        public async Task<List<Category>> GetCategories(string search)
        {
            IQueryable<Category> query = await _repositoryCategory.Query(p =>
                p.IsActive == true && p.Description.Contains(search));
            return query.ToList();
            List<Category> categoryList = await query.ToListAsync(); // Convert IQueryable to List asynchronously

            return categoryList;
        }
        public async Task<Purchase> Register(Purchase entity)
        {
            try
            {
                return await _repositoryPurchase.Register(entity);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Purchase>> PurchaseHistory(string PurchaseNumber, string StarDate, string EndDate)
        {
            IQueryable<Purchase> query = await _repositoryPurchase.Query();
            StarDate = StarDate is null ? "" : StarDate;
            EndDate = EndDate is null ? "" : EndDate;

            if (StarDate != "" && EndDate != "")
            {

                DateTime start_date = DateTime.ParseExact(StarDate, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime end_date = DateTime.ParseExact(EndDate, "dd/MM/yyyy", new CultureInfo("es-PE"));

                return query.Where(v =>
                    v.RegistrationDate.Value.Date >= start_date.Date &&
                    v.RegistrationDate.Value.Date <= end_date.Date
                )
                .Include(tdv => tdv.IdTypeDocumentNavigation)
                .Include(u => u.IdUsersNavigation)
                .Include(dv => dv.DetailPurchases)
                .ToList();
            }
            else
            {
                return query.Where(v => v.PurchaseNumber == PurchaseNumber)
                .Include(tdv => tdv.IdTypeDocumentNavigation)
                .Include(u => u.IdUsersNavigation)
                .Include(dv => dv.DetailPurchases)
                .ToList();
            }
        }
        public async Task<Purchase> Detail(string PurchaseNumber)
        {
            IQueryable<Purchase> query = await _repositoryPurchase.Query(v => v.PurchaseNumber == PurchaseNumber);

            return query
               .Include(tdv => tdv.IdTypeDocumentNavigation)
               .Include(u => u.IdUsersNavigation)
               .Include(dv => dv.DetailPurchases)
               .First();
        }
        public async Task<List<DetailPurchase>> Report(string StartDate, string EndDate)
        {
            DateTime start_date = DateTime.ParseExact(StartDate, "dd/MM/yyyy", new CultureInfo("es-PE"));
            DateTime end_date = DateTime.ParseExact(EndDate, "dd/MM/yyyy", new CultureInfo("es-PE"));

            List<DetailPurchase> lista = await _repositoryPurchase.Report(start_date, end_date);

            return lista;
        }
    }
}
