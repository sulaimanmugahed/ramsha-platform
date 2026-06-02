
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Inventory.Events;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.EventsHandlers;

public class InventoryItemQuantityChangeEventHandler : ILocalEventHandler<InventoryItemQuantityChangeEvent>
{

    public async Task HandleAsync(InventoryItemQuantityChangeEvent message, CancellationToken cancellationToken)
    {


    }


}
