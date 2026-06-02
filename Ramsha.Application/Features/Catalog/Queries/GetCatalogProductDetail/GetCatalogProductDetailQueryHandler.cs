
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Catalog;
using Ramsha.Application.Extensions;
using Ramsha.Application.Helpers;
using Ramsha.Application.Wrappers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogProductDetail;

public class GetCatalogProductDetailQueryHandler(
    IProductRepository productRepository,
    IRamshaCache redisCacheService
) : RamshaQueryHandler<GetCatalogProductDetailQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCatalogProductDetailQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.CatalogCacheKeys.GetProductDetailKey(request.ProductId.ToString());

        var productDto = await redisCacheService.GetOrCreateAsync<CatalogProductDetailDto>(
            key,
            async (ct) => await productRepository.GetProductCatalogDetail(new Domain.Products.ProductId(request.ProductId)),
            new RamshaCacheEntryOptions
            {
                Expiration = TimeSpan.FromHours(1)
            }, [
            CacheKeysHelper.ProductCacheKeys.Tag+request.ProductId,
            CacheKeysHelper.InventoryCacheKeys.Tag+request.ProductId]);

        return Success(productDto);
    }
}
