

using Ramsha.Domain.Inventory.Events;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.EventsHandlers;

public class InventoryItemUpdatedEventHandler(
) : ILocalEventHandler<InventoryItemUpdatedEvent>
{
    public async Task HandleAsync(InventoryItemUpdatedEvent message, CancellationToken cancellationToken)
    {

    }
}
