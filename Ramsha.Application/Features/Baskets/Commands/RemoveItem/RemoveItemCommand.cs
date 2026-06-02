
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Baskets.Commands.RemoveItem;

public class RemoveItemCommand : IRamshaCommand
{
    public Guid InventoryItemId { get; set; }
    public int Quantity { get; set; }

}
