using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Contracts.Caching;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Catalog;
using Ramsha.Application.DTOs.Common;
using Ramsha.Application.Helpers;
using Ramsha.Application.Wrappers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogProductsPaged;

public class GetCatalogProductsPagedQueryHandler(
    IProductRepository productRepository,
    IHttpService httpService,
    IRamshaCache redisCacheService
) : RamshaQueryHandler<GetCatalogProductsPagedQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCatalogProductsPagedQuery request, CancellationToken cancellationToken)
    {

        var key = CacheKeysHelper.CatalogCacheKeys.GetProductPagedKey(request);
        var result = await redisCacheService.GetOrCreateAsync<PaginationResponseDto<CatalogProductDto>?>(
            key,
            async (ct) => await productRepository.GetCatalogProductsPaged(request, p => p.Status == Domain.Products.Enums.ProductStatus.Active),
            new RamshaCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10)
            }, [CacheKeysHelper.ProductCacheKeys.Tag]);


        result.AddFilterMetaData(request.FilterParams);
        result.AddSortingMetaData(request.SortingParams);

        httpService.AddPagedHeader(result.MetaData);
        return Success(result.Data);
    }
}
