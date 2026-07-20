using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Contracts.Baskets.Commands;
using Ramsha.Domain.Baskets;
using Ramsha.Domain.Repositories;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Baskets.Commands.Delete
{
    public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : RamshaCommandHandler<DeleteBasketCommand>
    {
        public async override Task<IRamshaResult> HandleAsync(DeleteBasketCommand message, CancellationToken cancellationToken = default)
        {
            var basket = await basketRepository.FindAsync(new BasketId(message.Id));

            if (basket is null)
            {
                return Invalid(message: "no basket found for this id");
            }

            await basketRepository.DeleteAsync(basket);

            return Success();

        }
    }
}