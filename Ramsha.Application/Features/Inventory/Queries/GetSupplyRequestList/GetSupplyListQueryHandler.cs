
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.Application.Dtos.Supplies;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Inventory.Queries.GetSupplyRequestList;

public class GetSupplyRequestListQueryHandler(
    ISupplyRepository supplyRepository
) : RamshaQueryHandler<GetSupplyListQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetSupplyListQuery request, CancellationToken cancellationToken)
    {
        var supplyRequests = await supplyRepository.FindAllWithDetail(x => x.Status == request.Status);
        return Success(supplyRequests.Select(sr => sr.AsSupplyDto()).ToList());
    }
}
