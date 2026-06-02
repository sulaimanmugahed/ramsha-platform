using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Dtos.Supplies;
using Ramsha.Application.Features.Suppliers.Queries.GetSuppliesPaged;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Supplies.Queries.GetSuppliesPaged;

public class GetSuppliesPagedQueryHandler(
    ISupplyRepository supplyRepository,
    IHttpService httpService
) : RamshaQueryHandler<GetSuppliesPagedQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetSuppliesPagedQuery request, CancellationToken cancellationToken)
    {
        var response = await supplyRepository.GetSuppliesPaged(request);

        httpService.AddPagedHeader(response.MetaData);
        return Success(response.Data);
    }
}
