
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetSupplyRequestItem;

public class GetSupplyRequestItemQueryHandler(
    ISupplyRequestRepository supplyRequestRepository,
    IAuthenticatedUserService authenticatedUserService
) : RamshaQueryHandler<GetSupplyRequestItemQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetSupplyRequestItemQuery request, CancellationToken cancellationToken)
    {
        var supplyRequest = await supplyRequestRepository
      .GetWithDetails(x => x.Supplier == authenticatedUserService.UserName);

        if (supplyRequest is null)
            return Invalid(message: "no supplyRequest exist");

        return Success(supplyRequest.Items.FirstOrDefault(x => x.Id.Value == request.SupplyRequestItemId)?.AsRequestItemDto());
    }
}
