
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Queries.GetOrdersPaged;

public class GetOrdersPagedQueryHandler(
    IOrderRepository orderRepository,
    IHttpService httpService
) : RamshaQueryHandler<GetOrdersPagedQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetOrdersPagedQuery request, CancellationToken cancellationToken)
    {
        var response = await orderRepository.GetPaged(request);
        response.AddFilterMetaData(request.FilterParams);
        response.AddSortingMetaData(request.SortingParams);

        httpService.AddPagedHeader(response.MetaData);

        return Success(response.Data);
    }
}
