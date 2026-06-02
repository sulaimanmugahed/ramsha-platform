
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Inventory.Events;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.EventsHandlers;

public class InventoryItemPriceChangeEventHandler(
    IProductRepository productRepository
)
: ILocalEventHandler<InventoryItemPriceChangeEvent>
{
    public async Task HandleAsync(InventoryItemPriceChangeEvent message, CancellationToken cancellationToken)
    {


    }
}
