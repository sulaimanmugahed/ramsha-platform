
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.Commands.ApproveSupplyRequest;

public class ApproveSupplyCommand : IRamshaCommand
{
    public Guid SupplyId { get; set; }
}
