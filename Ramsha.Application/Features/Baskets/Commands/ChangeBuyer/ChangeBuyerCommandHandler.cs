using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Contracts.Baskets.Commands;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Baskets.Commands.ChangeBuyer
{
    public class ChangeBuyerCommandHandler : RamshaCommandHandler<ChangeBasketBuyerCommand>
    {
        public override Task<IRamshaResult> HandleAsync(ChangeBasketBuyerCommand message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}