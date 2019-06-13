
using System.Linq.Expressions;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Suppliers;
using Ramsha.Domain.Suppliers.Entities;
using Ramsha.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Ramsha.EntityFrameworkCore;

namespace Ramsha.Persistence.Repositories;

public class SupplyRequestRepository
: EFCoreRepository<ApplicationDbContext, SupplyRequest, SupplyRequestId>,
ISupplyRequestRepository
{
    public async Task<IEnumerable<SupplyRequest>> FindAllWithDetail(Expression<Func<SupplyRequest, bool>> criteria)
    {
        return await UnitOfWork(async () =>
{
  var context = await GetDbContextAsync();
  return await context.SupplyRequests.Where(criteria).Include(r => r.Items).ToListAsync();
});
    }

    public async Task<SupplyRequest?> GetWithDetails(Expression<Func<SupplyRequest, bool>> criteria)
    {
        return await UnitOfWork(async () =>
{
  var context = await GetDbContextAsync();
  return await context.SupplyRequests
    .Include(r => r.Items)
    .ThenInclude(r => r.SupplierVariant)
     .Include(r => r.Items)
    .ThenInclude(r => r.Product)
    .SingleOrDefaultAsync(criteria);
});
    }
}
