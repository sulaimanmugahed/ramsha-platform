using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Contracts.Baskets.Commands
{
    public record DeleteBasketCommand(Guid Id) : IRamshaCommand;
}