
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProductVariant;

public class GetCurrentSupplierProductVariantQueryHandler(
    ISupplierProductRepository supplierProductRepository,
    ISupplierRepository supplierRepository,
    IAuthenticatedUserService authenticatedUserService
) : RamshaQueryHandler<GetCurrentSupplierProductVariantQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentSupplierProductVariantQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (supplier is null)
            return Unauthenticated();

        var variant = await supplierProductRepository.GetVariantDetail(
           supplier.Id,
           new Domain.Products.ProductId(request.ProductId),
           new Domain.Products.ProductVariantId(request.VariantId)
        );

        return Success(variant?.AsSupplierVariantDto());
    }
}
