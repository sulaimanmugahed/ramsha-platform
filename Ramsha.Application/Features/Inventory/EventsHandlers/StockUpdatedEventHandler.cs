
using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Contracts.Caching;
using Ramsha.Application.Helpers;
using Ramsha.Caching;
using Ramsha.Domain.Inventory.Events;
using Ramsha.LocalMessaging.Abstractions;


namespace Ramsha.Application.Features.Inventory.EventsHandlers;

public class StockAddedEventHandler(
IBackgroundJobService backgroundJobService,
IRamshaCache cacheService
) : ILocalEventHandler<StockUpdatedEvent>
{
    public Task HandleAsync(StockUpdatedEvent message, CancellationToken cancellationToken)
    {
        backgroundJobService.Enqueue(() => InvalidateCache(message.ProductId.Value.ToString(), message.ProductVariantId.Value.ToString()));
        return Task.CompletedTask;
    }

    public void InvalidateCache(string productId, string variantId)
    {
        cacheService.RemoveByTagAsync([
            CacheKeysHelper.InventoryCacheKeys.Tag
            ]);
    }
}
