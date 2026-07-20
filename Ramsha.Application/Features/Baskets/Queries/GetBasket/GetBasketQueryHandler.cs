using Ramsha.Application.Constants;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Extensions;
using Ramsha.Application.Services;
using Ramsha.Application.Wrappers;
using Ramsha.Contracts;
using Ramsha.Domain.Repositories;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Baskets.Queries.GetBasket;

public class GetBasketQueryHandler(
	IBasketRepository basketRepository)
	: RamshaQueryHandler<GetBasketQuery, RamshaResult<BasketDto?>>
{
	public override async Task<RamshaResult<BasketDto?>> HandleAsync(GetBasketQuery request, CancellationToken cancellationToken)
	{
		var basket = await basketRepository.FindByBuyer(request.Buyer);
		return RamshaResults.Success(basket?.ToDto());
	}
}
