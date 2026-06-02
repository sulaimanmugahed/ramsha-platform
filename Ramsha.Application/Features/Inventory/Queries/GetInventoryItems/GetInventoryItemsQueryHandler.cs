
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Inventory;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Inventory.Queries.GetInventoryItems;

public class GetInventoryItemsQueryHandler(
    IInventoryItemRepository inventoryItemRepository,
    IHttpService httpService
) : RamshaQueryHandler<GetInventoryItemsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        var responseDto = await inventoryItemRepository.GetItemsPagedListAsync(
            request.PaginationParams ?? new(),
            request.SortingParams,
            request.FilterParams);

        responseDto.AddFilterMetaData(request.FilterParams);
        responseDto.AddSortingMetaData(request.SortingParams);

        httpService.AddPagedHeader(responseDto.MetaData);
        return Success(responseDto.Data);
    }
}
