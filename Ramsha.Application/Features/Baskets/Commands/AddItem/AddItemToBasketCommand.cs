using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;
namespace Ramsha.Application.Features.Baskets.Commands.AddItem;

public class AddItemToBasketCommand : IRamshaCommand
{
    public Guid InventoryItemId { get; set; }
    public int Quantity { get; set; }

}
