
using Ramsha.Domain.Inventory.Events;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.Events;

public class InventoryItemCreatedEventHandler(

) : ILocalEventHandler<InventoryItemCreatedEvent>
{
    public async Task HandleAsync(InventoryItemCreatedEvent message, CancellationToken cancellationToken)
    {

    }
}
