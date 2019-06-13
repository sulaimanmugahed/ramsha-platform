using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Baskets;
using Ramsha.Domain.Customers.Entities;
using Ramsha.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

using Ramsha.EntityFrameworkCore;

namespace Ramsha.Persistence.Repositories;

public class BasketRepository
	: EFCoreRepository<ApplicationDbContext, Basket, BasketId>
	, IBasketRepository
{


	public async Task<Basket?> FindByBuyer(string buyer)
	{
		return await UnitOfWork(async () =>
	   {
		   var dbContext = await GetDbContextAsync();
		   return await dbContext.Baskets.AsSplitQuery()
			.Include(b => b.Items)
			.ThenInclude(x => x.InventoryItem)
			.FirstOrDefaultAsync(b => b.Buyer == buyer);
	   });
	}

	public async Task<Basket?> GetDetail(string buyer)
	{
		return await UnitOfWork(async () =>
	   {
		   var dbContext = await GetDbContextAsync();
		   return await dbContext.Baskets.AsSplitQuery()
			.Include(b => b.Items)

				.ThenInclude(i => i.InventoryItem)
				.ThenInclude(x => x.ProductVariant)
				.ThenInclude(x => x.Dimensions)
				.Include(b => b.Items)
				.ThenInclude(i => i.InventoryItem)
				.ThenInclude(x => x.Supplier)
				.Include(b => b.Items)
				.ThenInclude(i => i.InventoryItem)
				.ThenInclude(i => i.Stocks)
				.Include(b => b.Items)
				.ThenInclude(i => i.InventoryItem)
				.ThenInclude(i => i.SupplierVariant)
				.FirstOrDefaultAsync(b => b.Buyer == buyer);
	   });
	}


}
