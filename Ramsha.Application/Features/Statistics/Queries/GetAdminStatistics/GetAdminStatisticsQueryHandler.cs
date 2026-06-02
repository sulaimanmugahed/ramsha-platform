
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Statistics;
using Ramsha.Application.Helpers;
using Ramsha.Application.Wrappers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Statistics.Queries.GetAdminStatistics;

public class GetAdminStatisticsQueryHandler(
IOrderRepository orderRepository,
IRamshaCache cacheService,
ICategoryRepository categoryRepository,
ICustomerRepository customerRepository,
ISupplierRepository supplierRepository,
IProductRepository productRepository
) : RamshaQueryHandler<GetAdminStatisticsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetAdminStatisticsQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.StatisticsCacheKeys.GetAdminStatisticsKey();
        var adminStatistics = await cacheService.GetOrCreateAsync<AdminStatistics>(
            key,
            async (ct) =>
            {
                var orders = await orderRepository.GetListAsync(x => x.OrderStatus == Domain.Orders.Enums.OrderStatus.Completed, [x => x.OrderItems]);
                var customers = await customerRepository.GetListAsync();
                var suppliers = await supplierRepository.GetListAsync();
                var products = await productRepository.GetListAsync();

                var salesPerformance = orders
                 .GroupBy(o => new { o.OrderDate.Month, o.OrderDate.Year })
                 .Select(g => new SalesPerformance
                 (new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                      g.Sum(o => o.OrderItems.Sum(x => x.Markup))
                 ))
                 .OrderBy(g => g.Month)
                 .ToList();

                var totalCategoriesProducts = await categoryRepository.GetTotalMainCategoriesProducts();

                return new AdminStatistics(
                orders.Count(),
                customers.Count,
                suppliers.Count,
                products.Count,
               salesPerformance,
               totalCategoriesProducts
                );
            },
            new RamshaCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10)
            }, [CacheKeysHelper.ProductCacheKeys.Tag, CacheKeysHelper.InventoryCacheKeys.Tag]);

        return Success(adminStatistics);
    }
}
