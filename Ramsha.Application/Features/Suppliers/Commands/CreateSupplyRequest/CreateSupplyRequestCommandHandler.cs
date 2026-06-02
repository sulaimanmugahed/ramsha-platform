using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Suppliers.Entities;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Commands.CreateSupplyRequest;

public class CreateSupplyRequestCommandHandler(
    ISupplyRequestRepository supplyRequestRepository,
    ISupplierRepository supplierRepository,
    IAuthenticatedUserService authenticatedUser
)
 : RamshaCommandHandler<CreateSupplyRequestCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateSupplyRequestCommand request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindByUsername(authenticatedUser.UserName);
        if (supplier is null)
            return Unauthenticated();

        var supplyRequest = SupplyRequest.Create(supplier.Username);


        await supplyRequestRepository.AddAsync(supplyRequest);

        return Success(supplyRequest.Id.Value.ToString());
    }
}
