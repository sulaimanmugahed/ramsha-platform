
using Ramsha.Application.Dtos.Inventory;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Enums;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.Commands.ApplyInventoryItemDiscount;

public class ApplyInventoryItemDiscountCommand : IRamshaCommand
{
    public Guid InventoryItemId { get; set; }
    public DiscountRequest Discount { get; set; }
}
