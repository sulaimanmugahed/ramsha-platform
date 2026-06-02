
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Commands.UpdateSupplyRequestItem;

public class UpdateSupplyRequestItemCommand : IRamshaCommand
{
    public Guid SupplyRequestItemId { get; set; }
    public int Quantity { get; set; }

}
