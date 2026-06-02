
using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Contracts.Caching;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Catalog;
using Ramsha.Application.Helpers;
using Ramsha.Application.Wrappers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogCategories;

public class GetCatalogCategoriesQueryHandler(
    ICategoryRepository categoryRepository,
    IRamshaCache redisCacheService
) : RamshaQueryHandler<GetCatalogCategoriesQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCatalogCategoriesQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.CatalogCacheKeys.GetCategoriesKey();
        var categoriesDTOs = await redisCacheService.GetOrCreateAsync<List<CatalogCategoryDto>>(
            key,
            async (ct) => await categoryRepository.GetCatalogCategories(),
            new RamshaCacheEntryOptions
            {
                Expiration = TimeSpan.FromDays(1),
            }
           );

        return Success(categoriesDTOs);
    }
}
