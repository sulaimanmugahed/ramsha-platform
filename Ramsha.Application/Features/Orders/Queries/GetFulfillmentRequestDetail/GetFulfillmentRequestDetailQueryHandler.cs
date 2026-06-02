
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Queries.GetFulfillmentRequestDetail;

public class GetFulfillmentRequestDetailQueryHandler(
    IFulfillmentRequestRepository fulfillmentRequestRepository
) : RamshaQueryHandler<GetFulfillmentRequestDetailQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetFulfillmentRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var fulfillRequest = await fulfillmentRequestRepository.FindAsync(
         x => x.Id == new Domain.Orders.FulfillmentRequestId(request.Id),
         [x => x.Order,
         x=> x.Items]
         );

        return Success(fulfillRequest?.AsFulfillmentRequestDetailDto());
    }
}
