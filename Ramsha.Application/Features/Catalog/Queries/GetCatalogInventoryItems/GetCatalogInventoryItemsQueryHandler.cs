
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

namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogInventoryItems;

public class GetCatalogInventoryItemsQueryHandler(
    IInventoryItemRepository inventoryItemRepository,
    IHttpService httpService,
    IRamshaCache redisCacheService
) : RamshaQueryHandler<GetCatalogInventoryItemsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCatalogInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.CatalogCacheKeys.GetInventoryItemsKey(
         request.ProductId.ToString(),
         request.ProductVariantId.ToString(),
         new PagedParams
         {
             PaginationParams = request.PaginationParams,
             SortingParams = request.SortingParams,
             FilterParams = request.FilterParams,
         });

        var responseDto = await redisCacheService.GetOrCreateAsync<PaginationResponseDto<CatalogInventoryItemDetailDto>>(
            key,
            async (ct) => await inventoryItemRepository.GetCatalogItemsPagedListAsync(
                          new Domain.Products.ProductId(request.ProductId),
                          request.PaginationParams,
                          request.SortingParams,
                          request.FilterParams,
                          request.ProductVariantId.HasValue ? new Domain.Products.ProductVariantId(request.ProductVariantId.Value) : null
                          ),
            new RamshaCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10)
            }, [CacheKeysHelper.InventoryCacheKeys.Tag, CacheKeysHelper.ProductCacheKeys.Tag + request.ProductId]);


        responseDto.AddFilterMetaData(request.FilterParams);
        responseDto.AddSortingMetaData(request.SortingParams);

        httpService.AddPagedHeader(responseDto.MetaData);
        return Success(responseDto.Data);
    }
}
