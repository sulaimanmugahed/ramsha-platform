
using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Contracts.Caching;
using Ramsha.Application.Helpers;
using Ramsha.Caching;
using Ramsha.Domain.Products.Events;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.EventsHandlers;

public class ProductDataChangedEventHandler(IBackgroundJobService backgroundJobService, IRamshaCache cacheService) : ILocalEventHandler<ProductDataChangedEvent>
{
    public Task HandleAsync(ProductDataChangedEvent message, CancellationToken cancellationToken)
    {
        backgroundJobService.Enqueue(() => InvalidateRelatedCachedData(message.ProductId.Value.ToString()));
        return Task.CompletedTask;
    }

    public void InvalidateRelatedCachedData(string productId)
    {
        cacheService.RemoveByTagAsync([
            CacheKeysHelper.ProductCacheKeys.Tag,
            CacheKeysHelper.ProductCacheKeys.Tag + productId
            ]);
    }
}
