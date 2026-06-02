
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Queries.GetCurrentDeliveryAgentFulfillments;

public class GetCurrentDeliveryAgentFulfillmentsQueryHandler(
    IDeliveryAgentRepository deliveryAgentRepository,
    IAuthenticatedUserService authenticatedUserService,
    IFulfillmentRequestRepository fulfillmentRequestRepository,
    IHttpService httpService
) : RamshaQueryHandler<GetCurrentDeliveryAgentFulfillmentsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentDeliveryAgentFulfillmentsQuery request, CancellationToken cancellationToken)
    {
        var agent = await deliveryAgentRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (agent is null)
            return Unauthenticated();

        var responseDto = await fulfillmentRequestRepository.GetPaged(request.PaginationParams, request.FilterParams, request.SortingParams, x => x.DeliveryAgentId == agent.Id);

        responseDto.AddFilterMetaData(request.FilterParams);
        responseDto.AddSortingMetaData(request.SortingParams);

        httpService.AddPagedHeader(responseDto.MetaData);

        return Success(responseDto.Data);
    }
}
