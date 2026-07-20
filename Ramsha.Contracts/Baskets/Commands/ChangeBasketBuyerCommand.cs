using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Contracts.Baskets.Commands
{
    public record ChangeBasketBuyerCommand(Guid Id, string NewBuyer) : IRamshaCommand;
}