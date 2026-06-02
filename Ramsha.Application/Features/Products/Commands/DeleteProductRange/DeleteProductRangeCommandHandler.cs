using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products;

using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Caching;
using Ramsha.Application.Helpers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.DeleteProductRange;

public class DeleteProductRangeCommandHandler(
    IProductRepository productRepository,
       IRamshaCache cacheService,
    IBackgroundJobService backgroundJobService
) : RamshaCommandHandler<DeleteProductRangeCommand>
{
    public override async Task<IRamshaResult> HandleAsync(DeleteProductRangeCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.Products
        .Select(p => new ProductId(p))
        .ToList();

        await productRepository.RemoveRange(productIds);

        backgroundJobService.Enqueue(() => cacheService.RemoveByTagAsync(CacheKeysHelper.ProductCacheKeys.Tag));


        return Success();
    }

    public void InvalidateCache()
    {

    }
}
