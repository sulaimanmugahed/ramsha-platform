
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Helpers;
using Ramsha.Application.Services;
using Ramsha.Application.Wrappers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IBackgroundJobService backgroundJobService,
    IRamshaCache ramshaCache
) : RamshaCommandHandler<DeleteProductCommand>
{
    public override async Task<IRamshaResult> HandleAsync(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindAsync(new Domain.Products.ProductId(request.Id));
        if (product is null)
            return Invalid(message: "no product not found with this id");

        await productRepository.DeleteAsync(product);

        backgroundJobService.Enqueue(() => ramshaCache.RemoveByTagAsync(new string[] {
             CacheKeysHelper.ProductCacheKeys.Tag,CacheKeysHelper.ProductCacheKeys.Tag + product.Id }));

        return Success();

    }
}
