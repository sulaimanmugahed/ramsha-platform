
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Commands.UpdateSupplyRequestItem;

public class UpdateSupplyRequestItemCommandHandler(
ISupplyRequestRepository supplyRequestRepository,
IAuthenticatedUserService authenticatedUserService

) : RamshaCommandHandler<UpdateSupplyRequestItemCommand>
{
    public override async Task<IRamshaResult> HandleAsync(UpdateSupplyRequestItemCommand request, CancellationToken cancellationToken)
    {
        var supplyRequest = await supplyRequestRepository
        .GetWithDetails(x => x.Supplier == authenticatedUserService.UserName);

        if (supplyRequest is null)
            return Invalid(message: "no supplyRequest exist");

        var item = supplyRequest.Items.FirstOrDefault(x => x.Id.Value == request.SupplyRequestItemId);
        if (item is null)
            return Invalid(message: "no item exist");

        item.SetQuantity(request.Quantity);


        return Success();
    }
}
