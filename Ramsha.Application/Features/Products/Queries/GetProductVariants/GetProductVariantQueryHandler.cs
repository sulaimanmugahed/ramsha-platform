
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;

using Ramsha.Application.Helpers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductVariants;

public class GetProductVariantQueryHandler(
    IProductRepository productRepository,
    IRamshaCache redisCacheService
) : RamshaQueryHandler<GetProductVariantQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductVariantQuery request, CancellationToken cancellationToken)
    {
        var key = CacheKeysHelper.ProductCacheKeys.GetProductVariantKey(request.ProductId.ToString(), request.VariantId.ToString());

        var variantDTO = await redisCacheService.GetOrCreateAsync<ProductVariantDto?>(
            key,
            async (ct) =>
            {
                var variant = await productRepository.GetVariantDetails(
                new Domain.Products.ProductId(request.ProductId),
                new Domain.Products.ProductVariantId(request.VariantId)
                );

                return variant?.AsDto();
            }, new RamshaCacheEntryOptions
            {
                Expiration = TimeSpan.FromHours(10)
            }, [CacheKeysHelper.ProductCacheKeys.Tag + request.ProductId]);

        return Success(variantDTO);
    }
}
