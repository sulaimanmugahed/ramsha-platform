using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Services;
using Ramsha.Contracts.Baskets.Commands;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Baskets.Commands.DeleteCurrent
{
    public class DeleteBasketCommandHandler(BasketService basketService) : RamshaCommandHandler<DeleteCurrentBasketCommand>
    {
        public async override Task<IRamshaResult> HandleAsync(DeleteCurrentBasketCommand message, CancellationToken cancellationToken = default)
        {
            await basketService.ClearBasket();
            return Success();
        }
    }
}