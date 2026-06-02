using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProductVariantList;

public class GetCurrentSupplierProductVariantListQueryHandler(
    ISupplierProductRepository supplierProductRepository,
    ISupplierRepository supplierRepository,
    IAuthenticatedUserService authenticatedUserService
) : RamshaQueryHandler<GetCurrentSupplierProductVariantListQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentSupplierProductVariantListQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (supplier is null)
            return Unauthenticated();

        var variants = await supplierProductRepository.GetVariantList(supplier.Id, new Domain.Products.ProductId(request.ProductId));
        return Success(variants.Select(x => x.AsSupplierVariantDto()).ToList());
    }
}
