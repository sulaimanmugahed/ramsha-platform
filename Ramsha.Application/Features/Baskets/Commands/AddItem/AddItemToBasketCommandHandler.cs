using Ramsha.Application.Constants;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.Contracts;
using Ramsha.Domain.Customers.Entities;
using Ramsha.Domain.Repositories;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Baskets.Commands.AddItem;

internal class AddItemToBasketCommandHandler(
	IInventoryItemRepository inventoryItemRepository,
	IBasketRepository basketRepository,
	IAuthenticatedUserService authenticatedUser)
	: RamshaCommandHandler<AddItemToBasketCommand, RamshaResult<AddItemToBasketResponse>>
{
	public override async Task<RamshaResult<AddItemToBasketResponse>> HandleAsync(AddItemToBasketCommand request, CancellationToken cancellationToken)
	{
		var inventoryItem = await inventoryItemRepository
		.FindAsync(x => x.Id == new Domain.Inventory.InventoryItemId(request.Input.InventoryItemId));

		if (inventoryItem is null)
			return RamshaResults.NotFound();

		var buyer = request.Buyer;

		var basket = await basketRepository.FindByBuyer(buyer);

		var response = new AddItemToBasketResponse();

		if (basket is null)
		{
			response.IsNewBasket = true;
			if (string.IsNullOrEmpty(buyer))
			{

				buyer = Guid.NewGuid().ToString();
				response.IsAnonBuyer = true;

			}
			var newBasket = Basket.Create(buyer);
			await basketRepository.AddAsync(newBasket, true);
			basket = newBasket;
		}

		response.Buyer = buyer;

		var basketItem = basket.AddItem(inventoryItem, request.Input.Quantity);

		response.Item = basketItem.AsItemDto();

		return RamshaResults.Success(response);
	}
}
