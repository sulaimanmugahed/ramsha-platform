using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;
namespace Ramsha.Application.Features.Baskets.Commands.AddItem;


public class AddItemToBasketResponse
{
    public BasketItemDto Item { get; set; }
    public bool IsNewBasket { get; set; }
    public bool IsAnonBuyer { get; set; }
    public string Buyer { get; set; }
};

public class AddItemToBasketInput
{
    public Guid InventoryItemId { get; set; }
    public int Quantity { get; set; }
}
public class AddItemToBasketCommand : IRamshaCommand<RamshaResult<AddItemToBasketResponse>>
{
    public AddItemToBasketInput Input { get; set; }
    public string? Buyer { get; set; }

}
