using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Suppliers;
using Ramsha.Domain.Suppliers.Entities;
using Ramsha.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.DTOs.Common;
using Ramsha.Domain.Products.Entities;
using Ramsha.Application.Wrappers;
using Ramsha.Application.Contracts;
using Ramsha.EntityFrameworkCore;


namespace Ramsha.Persistence.Repositories;

public class SupplierRepository
: EFCoreRepository<ApplicationDbContext, Supplier, SupplierId>, ISupplierRepository
{

	public async Task<Supplier?> FindByUsername(string username)
	{
		return await UnitOfWork(async () =>
{
var context = await GetDbContextAsync();
return await context.Suppliers.Include(x => x.Supplies).FirstOrDefaultAsync(x => x.Username == username);
});
	}

}
