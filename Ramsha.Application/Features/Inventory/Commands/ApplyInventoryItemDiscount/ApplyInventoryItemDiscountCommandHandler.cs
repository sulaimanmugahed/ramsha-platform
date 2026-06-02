
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Inventory.Commands.ApplyInventoryItemDiscount;

public class ApplyInventoryItemDiscountCommandHandler(
    IInventoryItemRepository inventoryItemRepository
) : RamshaCommandHandler<ApplyInventoryItemDiscountCommand>
{
    public override async Task<IRamshaResult> HandleAsync(ApplyInventoryItemDiscountCommand request, CancellationToken cancellationToken)
    {
        var inventoryItem = await inventoryItemRepository.GetWithStocksDetail(new Domain.Inventory.InventoryItemId(request.InventoryItemId));
        if (inventoryItem is null)
            return NotFound(message: "no item exist");

        var discount = Discount.Create(request.Discount.Value, request.Discount.StartDate, request.Discount.EndDate, request.Discount.Type);
        inventoryItem.ApplyDiscount(discount);

        return Success();
    }
}

