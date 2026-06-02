using Ramsha.Application.Constants;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Baskets.Queries.GetBasket;

public class GetBasketQueryHandler(
	IAuthenticatedUserService authenticatedUser,
	ICookieService cookieService,
	IBasketRepository basketRepository)
	: RamshaQueryHandler<GetBasketQuery>
{
	public override async Task<IRamshaResult> HandleAsync(GetBasketQuery request, CancellationToken cancellationToken)
	{
		var buyer = authenticatedUser.UserName ?? cookieService.GetCookieValue(ApplicationCookies.Buyer);

		if (buyer is null)
		{
			cookieService.RemoveCookie(ApplicationCookies.Buyer);
			return NotFound();
		}

		var basket = await basketRepository.FindByBuyer(buyer);


		return Success(basket?.ToDto());
	}
}
