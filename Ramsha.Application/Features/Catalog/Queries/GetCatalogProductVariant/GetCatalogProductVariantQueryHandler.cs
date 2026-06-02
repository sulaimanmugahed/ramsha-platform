
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Catalog;
using Ramsha.Application.Helpers;
using Ramsha.Application.Wrappers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogProductVariant;

public class GetCatalogProductVariantQueryHandler(
    IProductRepository productRepository,
    IRamshaCache redisCacheService
) : RamshaQueryHandler<GetCatalogProductVariantQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCatalogProductVariantQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.CatalogCacheKeys.GetCatalogVariantKey(request.ProductId.ToString(), request.ProductVariantId.ToString());
        var catalogVariantDto = await redisCacheService.GetOrCreateAsync<CatalogVariantDto>(
            key,
            async (ct) => await productRepository.GetCatalogProductVariant(
                        new Domain.Products.ProductId(request.ProductId),
                        request.ProductVariantId.HasValue ? new Domain.Products.ProductVariantId(request.ProductVariantId.Value) : null
                        ),
                        new RamshaCacheEntryOptions
                        {
                            Expiration = TimeSpan.FromHours(1)
                        }, [CacheKeysHelper.ProductCacheKeys.Tag + request.ProductId]);

        return Success(catalogVariantDto);
    }
}
