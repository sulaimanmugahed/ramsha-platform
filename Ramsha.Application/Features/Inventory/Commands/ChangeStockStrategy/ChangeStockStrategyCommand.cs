
using Ramsha.Domain.Inventory.Enums;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.Commands.ChangeStockStrategy;

public class ChangeStockStrategyCommand : IRamshaCommand
{
    public StockSelectionType SelectionType { get; set; }
    public Guid InventoryItemId { get; set; }
}
