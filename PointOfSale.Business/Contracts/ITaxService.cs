using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Business.Contracts
{
	public interface ITaxService
	{
		Task<List<Tax>> List();
		Task<List<Tax>> ListForProducts();
		Task<Tax> Add(Tax entity);
		Task<Tax> Edit(Tax entity);
		Task<bool> Delete(int idTax);
	}
}
