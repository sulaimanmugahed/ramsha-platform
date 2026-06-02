
using Ramsha.Application.Dtos.Inventory;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.Queries.GetInventoryItems;

public class GetInventoryItemsQuery : IRamshaQuery
{
    public Ramsha.Application.Wrappers.PaginationParams? PaginationParams { get; set; }
    public SortingParams? SortingParams { get; set; }
    public FilterParams? FilterParams { get; set; }
}
