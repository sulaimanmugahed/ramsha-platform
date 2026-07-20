using Ramsha.Common.Domain;
using Ramsha.Domain.Customers.Entities;
using Ramsha.Domain.Suppliers;
using Ramsha.Domain.Suppliers.Entities;

namespace Ramsha.Domain.Repositories;

public interface ISupplierRepository : IRepository<Supplier, SupplierId>
{
	Task<Supplier?> FindByUsername(string username);
}
