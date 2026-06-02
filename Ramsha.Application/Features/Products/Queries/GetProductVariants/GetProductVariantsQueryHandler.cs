
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;

using Ramsha.Application.Helpers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductVariants;

public class GetProductVariantsQueryHandler(
    IProductRepository productRepository,
    IRamshaCache redisCacheService

) : RamshaQueryHandler<GetProductVariantsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductVariantsQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.ProductCacheKeys.GetProductVariantsKey(request.ProductId.ToString());

        var variantDTOs = await redisCacheService.GetOrCreateAsync<List<ProductVariantDto?>>(
            key,
            async (ct) =>
            {
                var variants = await productRepository.GetVariants(
                       new Domain.Products.ProductId(request.ProductId)
                      );

                return variants.Select(v => v?.AsDto()).ToList();
            },
            new RamshaCacheEntryOptions
            {
                Expiration = TimeSpan.FromHours(10)
            }, [CacheKeysHelper.ProductCacheKeys.Tag + request.ProductId]);

        return Success(variantDTOs);
    }
}
