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
	public class TaxService : ITaxService
	{
		private readonly IGenericRepository<Tax> _repository;

		public TaxService(IGenericRepository<Tax> repository)
        {
			_repository = repository;
		}
		public async Task<List<Tax>> List()
		{
			IQueryable<Tax> query = await _repository.Query();
			return query.AsNoTracking().ToList();
		}
		
		public async Task<List<Tax>> ListForProducts()
		{
			IQueryable<Tax> query = await _repository.Query();
			List<Tax> taxes = await query
				.Where(dv => dv.IsActive == true /*&& dv.IsFixed == false*/)
				.AsNoTracking()
				.ToListAsync();

			return taxes;
		}
		public async Task<Tax> Add(Tax entity)
		{
			Tax Tax_exists = await _repository.Get(u => u.Percentage == entity.Percentage);

			if (Tax_exists != null)
				throw new TaskCanceledException("The percentage already exists");

			try
			{

				Tax Tax_created = await _repository.Add(entity);

				if (Tax_created.IdTax == 0)
					throw new TaskCanceledException("Failed to create Tax");

				IQueryable<Tax> query = await _repository.Query(u => u.IdTax == Tax_created.IdTax);
				Tax_created = query.First();

				return Tax_created;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<Tax> Edit(Tax entity)
		{

			Tax Tax_exists = await _repository.Get(u => u.Percentage == entity.Percentage && u.IdTax != entity.IdTax);

			if (Tax_exists != null)
				throw new TaskCanceledException("The percentage already exists");

			try
			{
				IQueryable<Tax> queryTax = await _repository.Query(u => u.IdTax == entity.IdTax);

				Tax Tax_edit = queryTax.First();

				Tax_edit.Percentage = entity.Percentage;
				Tax_edit.Description = entity.Description;
				Tax_edit.IsFixed = entity.IsFixed;
				Tax_edit.IsExternal = entity.IsExternal;
				Tax_edit.IsActive = entity.IsActive;

				bool response = await _repository.Edit(Tax_edit);
				if (!response)
					throw new TaskCanceledException("Could not modify Tax");

				Tax Tax_edited = queryTax.First();

				return Tax_edited;
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
				Tax Tax_found = await _repository.Get(u => u.IdTax == idTax);

				if (Tax_found == null)
					throw new TaskCanceledException("Tax does not exist");

				bool response = await _repository.Delete(Tax_found);

				return response;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
