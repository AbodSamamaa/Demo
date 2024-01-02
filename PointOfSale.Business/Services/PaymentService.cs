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
	public class PaymentService : IPaymentService
	{
		private readonly IGenericRepository<Payment> _repository;

		public PaymentService(IGenericRepository<Payment> repository)
		{
			_repository = repository;
		}
		public async Task<List<Payment>> List()
		{
			IQueryable<Payment> query = await _repository.Query();
			return query.AsNoTracking().ToList();
		}

		public async Task<List<Payment>> ListForActive()
		{
			IQueryable<Payment> query = await _repository.Query();
			List<Payment> Paymentes = await query
				.Where(dv => dv.IsActive == true)
				.AsNoTracking()
				.ToListAsync();

			return Paymentes;
		}
		public async Task<Payment> Add(Payment entity)
		{
			Payment Payment_exists = await _repository.Get(u => u.IdPayment == entity.IdPayment);

			if (Payment_exists != null)
				throw new TaskCanceledException("The payment way exists");

			try
			{
                
                Payment Payment_created = await _repository.Add(entity);

				if (Payment_created.IdPayment == 0)
					throw new TaskCanceledException("Failed to create Payment");

				IQueryable<Payment> query = await _repository.Query(u => u.IdPayment == Payment_created.IdPayment);
				Payment_created = query.First();

				return Payment_created;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<Payment> Edit(Payment entity)
		{

			Payment Payment_exists = await _repository.Get(u => u.IsActive == entity.IsActive && u.IdPayment != entity.IdPayment);

			if (Payment_exists == null)
				throw new TaskCanceledException("The payment way is not exists");

			try
			{
				IQueryable<Payment> queryPayment = await _repository.Query(u => u.IdPayment == entity.IdPayment);

				Payment Payment_edit = queryPayment.First();

				Payment_edit.PaymentMethod = entity.PaymentMethod;
				Payment_edit.Description = entity.Description;
				Payment_edit.IsActive = entity.IsActive;

				bool response = await _repository.Edit(Payment_edit);
				if (!response)
					throw new TaskCanceledException("Could not modify Payment way");

				Payment Payment_edited = queryPayment.First();

				return Payment_edited;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<bool> Delete(int idPayment)
		{
			try
			{
				Payment Payment_found = await _repository.Get(u => u.IdPayment == idPayment);

				if (Payment_found == null)
					throw new TaskCanceledException("Payment way does not exist");

				bool response = await _repository.Delete(Payment_found);

				return response;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
