
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Inventory.Commands.ChangeStockStrategy;

public class ChangeStockStrategyCommandHandler(
    IInventoryItemRepository inventoryItemRepository
) : RamshaCommandHandler<ChangeStockStrategyCommand>
{
    public override async Task<IRamshaResult> HandleAsync(ChangeStockStrategyCommand request, CancellationToken cancellationToken)
    {
        var item = await inventoryItemRepository.FindAsync(x => x.Id == new Domain.Inventory.InventoryItemId(request.InventoryItemId), [x => x.Stocks]);
        if (item is null)
            return NotFound(message: "no item with this id");

        item.SetStockSelectionType(request.SelectionType);

        return Success();
    }
}
