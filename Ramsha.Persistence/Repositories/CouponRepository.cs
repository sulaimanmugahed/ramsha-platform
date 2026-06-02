
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Products;
using Ramsha.Domain.Products.Entities;
using Ramsha.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Ramsha.EntityFrameworkCore;

namespace Ramsha.Persistence.Repositories;

public class CouponRepository
: EFCoreRepository<ApplicationDbContext, Coupon, CouponId>,
ICouponRepository
{

    public async Task<Coupon?> GetByCodeAsync(string code)
    {
        var dbContext = await GetDbContextAsync();
        return await dbContext.Coupons.SingleOrDefaultAsync(c => c.Code == code);
    }
}
