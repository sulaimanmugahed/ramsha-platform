
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Commands.RemoveSupplyRequestItem;

public class RemoveSupplyRequestItemCommand : IRamshaCommand
{
    public Guid SupplyRequestItemId { get; set; }
}
