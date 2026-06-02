using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Supplies;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Supplies.Queries.GetSupplyDetail;

public class GetSupplyDetailQueryHandler(
    ISupplyRepository supplyRepository
) : RamshaQueryHandler<GetSupplyDetailQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetSupplyDetailQuery request, CancellationToken cancellationToken)
    {
        var supply = await supplyRepository.GetWithDetails(x => x.Id == new Domain.Suppliers.SupplyId(request.Id));
        if (supply is null)
            return NotFound(message: "no supply with this id");

        return Success(supply.AsSupplyDetailDto());
    }
}
