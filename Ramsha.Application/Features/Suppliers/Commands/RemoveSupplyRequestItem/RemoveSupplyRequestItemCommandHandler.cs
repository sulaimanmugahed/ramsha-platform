
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Commands.RemoveSupplyRequestItem;

public class RemoveSupplyRequestItemCommandHandler(
    ISupplyRequestRepository supplyRequestRepository,
    IAuthenticatedUserService authenticatedUserService
) : RamshaCommandHandler<RemoveSupplyRequestItemCommand>
{
    public override async Task<IRamshaResult> HandleAsync(RemoveSupplyRequestItemCommand request, CancellationToken cancellationToken)
    {
        var supplyRequest = await supplyRequestRepository
       .GetWithDetails(x => x.Supplier == authenticatedUserService.UserName);

        if (supplyRequest is null)
            return Invalid(message: "no supplyRequest exist");

        supplyRequest.RemoveItem(new Domain.Suppliers.SupplyRequestItemId(request.SupplyRequestItemId));

        return Success();
    }
}

