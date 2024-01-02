using Microsoft.EntityFrameworkCore;
using PointOfSale.Business.Contracts;
using PointOfSale.Data.Repository;
using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Business.Services
{
    public class TypeDocumentPurchaseService : ITypeDocumentPurchaseService
    {
        private readonly IGenericRepository<TypeDocumentPurchase> _repository;

        public TypeDocumentPurchaseService(IGenericRepository<TypeDocumentPurchase> repository)
        {
            _repository = repository;
        }

        public async Task<List<TypeDocumentPurchase>> List()
        {
            IQueryable<TypeDocumentPurchase> query = await _repository.Query();
            return query.ToList();
        }
		public async Task<List<TypeDocumentPurchase>> ListForTypeDocumentPurchases()
		{
			IQueryable<TypeDocumentPurchase> query = await _repository.Query();
			List<TypeDocumentPurchase> TypeDocumentPurchases = await query
				.Where(dv => dv.IsActive == true)
				.ToListAsync(); // Remove the erroneous .AsNoTracking()

			return TypeDocumentPurchases;
		}
		public async Task<TypeDocumentPurchase> Add(TypeDocumentPurchase entity)
        {
            TypeDocumentPurchase TypeDocumentPurchase_exists = await _repository.Get(u => u.IdTypeDocumentPurchase == entity.IdTypeDocumentPurchase);

            if (TypeDocumentPurchase_exists != null)
                throw new TaskCanceledException("The Type Document Purchase already exists");

            try
            {

                TypeDocumentPurchase TypeDocumentPurchase_created = await _repository.Add(entity);

                if (TypeDocumentPurchase_created.IdTypeDocumentPurchase == 0)
                    throw new TaskCanceledException("Failed to create Type Document Purchase");

                IQueryable<TypeDocumentPurchase> query = await _repository.Query(u => u.IdTypeDocumentPurchase == TypeDocumentPurchase_created.IdTypeDocumentPurchase);
                TypeDocumentPurchase_created = query.First();

                return TypeDocumentPurchase_created;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TypeDocumentPurchase> Edit(TypeDocumentPurchase entity)
        {

            TypeDocumentPurchase TypeDocumentPurchase_exists = await _repository.Get(u => u.IdTypeDocumentPurchase == entity.IdTypeDocumentPurchase && u.Description != entity.Description);

            if (TypeDocumentPurchase_exists != null)
                throw new TaskCanceledException("The Type Document Purchase already exists");

            try
            {
                IQueryable<TypeDocumentPurchase> queryTypeDocumentPurchase = await _repository.Query(u => u.IdTypeDocumentPurchase == entity.IdTypeDocumentPurchase);

                TypeDocumentPurchase TypeDocumentPurchase_edit = queryTypeDocumentPurchase.First();

                TypeDocumentPurchase_edit.Description = entity.Description;
                TypeDocumentPurchase_edit.IsActive = entity.IsActive;

                bool response = await _repository.Edit(TypeDocumentPurchase_edit);
                if (!response)
                    throw new TaskCanceledException("Could not modify Type Document Purchase");

                TypeDocumentPurchase TypeDocumentPurchase_edited = queryTypeDocumentPurchase.First();

                return TypeDocumentPurchase_edit;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int idTax)
        {
            try
            {
                TypeDocumentPurchase TypeDocumentPurchase_found = await _repository.Get(u => u.IdTypeDocumentPurchase == idTax);

                if (TypeDocumentPurchase_found == null)
                    throw new TaskCanceledException("Type Document Purchase does not exist");

                bool response = await _repository.Delete(TypeDocumentPurchase_found);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
