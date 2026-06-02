
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProducts;

public class GetCurrentSupplierProductsQueryHandler(
    ISupplierProductRepository supplierProductRepository,
    ISupplierRepository supplierRepository,
    IAuthenticatedUserService authenticatedUserService,
    IHttpService httpService
) : RamshaQueryHandler<GetCurrentSupplierProductsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentSupplierProductsQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (supplier is null)
            return Unauthenticated();

        var result = await supplierProductRepository.GetPaged(supplier.Id, request.PaginationParams);

        httpService.AddPagedHeader(result.MetaData);

        return Success(result.Data);

    }
}
