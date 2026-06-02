using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Wrappers;
using Ramsha.Application.Contracts.Caching;
using Ramsha.Application.DTOs.Common;
using Ramsha.Application.Helpers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductsPaged;

public class GetProductsPagedQueryHandler(
    IProductRepository productRepository,
    IHttpService httpService,
    IRamshaCache redisCacheService
) : RamshaQueryHandler<GetProductsPagedQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductsPagedQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.ProductCacheKeys.GetProductPagedKey(request);
        var result = await redisCacheService.GetOrCreateAsync<PaginationResponseDto<ProductDto>>(
            key,
            async (ct) => await productRepository.GetProductsPaged(
               request.PaginationParams,
               request.FilterParams,
               request.SortingParams),
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
