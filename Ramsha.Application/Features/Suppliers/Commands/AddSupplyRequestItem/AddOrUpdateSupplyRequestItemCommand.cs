
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Commands.AddSupplyRequestItem;

public class AddOrUpdateSupplyRequestItemCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public Guid ProductVariantId { get; set; }
    public int Quantity { get; set; }
}
