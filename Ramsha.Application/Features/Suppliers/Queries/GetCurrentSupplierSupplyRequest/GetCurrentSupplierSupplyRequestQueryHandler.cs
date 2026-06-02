
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierSupplyRequest;

public class GetCurrentSupplierSupplyRequestQueryHandler(
    ISupplyRequestRepository supplyRequestRepository,
    IAuthenticatedUserService authenticatedUserService
) : RamshaQueryHandler<GetCurrentSupplierSupplyRequestQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentSupplierSupplyRequestQuery request, CancellationToken cancellationToken)
    {
        var supplyRequest = await supplyRequestRepository.GetWithDetails(
            x => x.Supplier == authenticatedUserService.UserName);

        return Success(supplyRequest?.AsSupplyRequestDto());
    }
}
