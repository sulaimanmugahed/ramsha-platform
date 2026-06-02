using Ramsha.Application.Constants;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Customers.Entities;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Baskets.Commands.AddItem;

internal class AddItemToBasketCommandHandler(
	IInventoryItemRepository inventoryItemRepository,
	IBasketRepository basketRepository,
	IAuthenticatedUserService authenticatedUser,
	ICookieService cookieService)
	: RamshaCommandHandler<AddItemToBasketCommand>
{
	public override async Task<IRamshaResult> HandleAsync(AddItemToBasketCommand request, CancellationToken cancellationToken)
	{
		var inventoryItem = await inventoryItemRepository
		.FindAsync(x => x.Id == new Domain.Inventory.InventoryItemId(request.InventoryItemId));

		if (inventoryItem is null)
			return NotFound();

		var buyer = authenticatedUser.UserName ?? cookieService.GetCookieValue(ApplicationCookies.Buyer);

		var basket = await basketRepository.FindByBuyer(buyer);

		if (basket is null)
		{
			if (string.IsNullOrEmpty(buyer))
			{
				buyer = Guid.NewGuid().ToString();

				cookieService.SetCookieValue(
					ApplicationCookies.Buyer,
					buyer,
					new()
					{
						IsEssential = true,
						Expires = DateTime.UtcNow.AddDays(30)
					});
			}
			var newBasket = Basket.Create(buyer);
			await basketRepository.AddAsync(newBasket, true);
			basket = newBasket;
		}

		var basketItem = basket.AddItem(inventoryItem, request.Quantity);

		return Success(basketItem.AsItemDto());
	}
}
