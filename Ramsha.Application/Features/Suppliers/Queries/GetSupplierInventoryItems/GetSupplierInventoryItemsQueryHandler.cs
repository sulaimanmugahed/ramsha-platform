
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetSupplierInventoryItems;

public class GetSupplierInventoryItemsQueryHandler(
    IInventoryItemRepository inventoryItemRepository,
    IAuthenticatedUserService authenticatedUserService,
    ISupplierRepository supplierRepository
) : RamshaQueryHandler<GetSupplierInventoryItemsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetSupplierInventoryItemsQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (supplier is null)
            return Unauthenticated();

        var inventoryItems = await inventoryItemRepository.GetListAsync(x => x.SupplierId == supplier.Id);
        return Success(inventoryItems.Select(x => x.AsSupplierInventoryItemDto()).ToList());
    }
}
