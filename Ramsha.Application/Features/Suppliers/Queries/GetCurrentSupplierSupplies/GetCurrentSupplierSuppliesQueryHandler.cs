using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Queries.GetSupplyRequestsBySupplier;

public class GetCurrentSupplierSuppliesQueryHandler(
    ISupplyRepository supplyRepository,
    IAuthenticatedUserService authenticatedUser,
    IHttpService httpService
) : RamshaQueryHandler<GetCurrentSupplierSuppliesQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentSupplierSuppliesQuery request, CancellationToken cancellationToken)
    {
        if (authenticatedUser.UserName is null)
            return Unauthenticated();

        var responseDto = await supplyRepository
        .GetSuppliesPaged(
            new()
            {
                PaginationParams = request.PaginationParams,
                FilterParams = request.FilterParams,
                SortingParams = request.SortingParams
            }, x => x.Supplier == authenticatedUser.UserName);

        responseDto.AddFilterMetaData(request.FilterParams);
        responseDto.AddSortingMetaData(request.SortingParams);
        httpService.AddPagedHeader(responseDto.MetaData);

        return Success(responseDto.Data);
    }
}
