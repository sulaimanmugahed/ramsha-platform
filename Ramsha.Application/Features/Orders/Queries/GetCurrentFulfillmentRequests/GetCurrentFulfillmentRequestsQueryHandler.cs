
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Queries.GetCurrentSupplierFulfillmentRequests;

public class GetCurrentSupplierFulfillmentRequestsQueryHandler(
    IFulfillmentRequestRepository fulfillmentRequestRepository,
    ISupplierRepository supplierRepository,
    IAuthenticatedUserService authenticatedUserService,
    IHttpService httpService
) : RamshaQueryHandler<GetCurrentSupplierFulfillmentRequestsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentSupplierFulfillmentRequestsQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (supplier is null)
            return Unauthenticated();

        var responseDto = await fulfillmentRequestRepository.GetPaged(request.PaginationParams, request.FilterParams, request.SortingParams, x => x.SupplierId == supplier.Id);

        responseDto.AddFilterMetaData(request.FilterParams);
        responseDto.AddSortingMetaData(request.SortingParams);

        httpService.AddPagedHeader(responseDto.MetaData);

        return Success(responseDto.Data);

    }
}
