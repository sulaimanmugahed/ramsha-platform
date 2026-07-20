using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Services;
using Ramsha.Contracts.Baskets.Queries;
using Ramsha.LocalMessaging;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Baskets.Queries.GetBasketDeliveryFeeDetail
{
    public class GetBasketDetailQueryHandler(BasketService basketService) : RamshaQueryHandler<GetBasketDetailQuery>
    {
        public async override Task<IRamshaResult> HandleAsync(GetBasketDetailQuery message, CancellationToken cancellationToken = default)
        {
            return Success(await basketService.GetBasketDeliveryFeeDetail());
        }
    }
}