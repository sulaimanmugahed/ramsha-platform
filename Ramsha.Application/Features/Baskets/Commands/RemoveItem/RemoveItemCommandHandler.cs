using Ramsha.Application.Constants;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;

using Ramsha.LocalMessaging;


namespace Ramsha.Application.Features.Baskets.Commands.RemoveItem;

public class RemoveItemCommandHandler(
	IBasketRepository basketRepository,
	ICookieService cookieService,
	IAuthenticatedUserService authenticatedUser
	) : RamshaCommandHandler<RemoveItemCommand>
{
	public override async Task<IRamshaResult> HandleAsync(RemoveItemCommand request, CancellationToken cancellationToken)
	{
		var buyer = authenticatedUser.UserName ?? cookieService.GetCookieValue(ApplicationCookies.Buyer);
		if (buyer is null)
			return NotFound(message: "no buyer");

		var basket = await basketRepository.FindByBuyer(buyer);

		if (basket is null)
			return NotFound(message: "no basket");

		basket.RemoveItem(new Domain.Inventory.InventoryItemId(request.InventoryItemId), request.Quantity);


		return Success();

	}
}
