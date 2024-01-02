using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Business.Contracts
{
	public interface IPaymentService
	{
		Task<List<Payment>> List();
		Task<List<Payment>> ListForActive();
		Task<Payment> Add(Payment entity);
		Task<Payment> Edit(Payment entity);
		Task<bool> Delete(int idPayment);
	}
}
