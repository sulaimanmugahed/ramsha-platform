using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Queries.GetAllFulfillmentRequestsPaged;

public class GetAllFulfillmentRequestsPagedQueryHandler(
    IFulfillmentRequestRepository fulfillmentRequestRepository,
    IHttpService httpService
) : RamshaQueryHandler<GetAllFulfillmentRequestsPagedQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetAllFulfillmentRequestsPagedQuery request, CancellationToken cancellationToken)
    {
        var responseDto = await fulfillmentRequestRepository.GetPaged(request.PaginationParams, request.FilterParams, request.SortingParams);

        responseDto.AddFilterMetaData(request.FilterParams);
        responseDto.AddSortingMetaData(request.SortingParams);

        httpService.AddPagedHeader(responseDto.MetaData);

        return Success(responseDto.Data);

    }
}
