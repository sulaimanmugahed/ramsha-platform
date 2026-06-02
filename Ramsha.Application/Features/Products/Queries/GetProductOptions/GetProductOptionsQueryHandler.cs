
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Extensions;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductOptions;

public class GetProductOptionsQueryHandler(
    IProductRepository productRepository
) : RamshaQueryHandler<GetProductOptionsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductOptionsQuery request, CancellationToken cancellationToken)
    {
        var options = await productRepository.GetProductOptions(new Domain.Products.ProductId(request.ProductId));

        return Success(options.Select(x => x.AsProductOption()).ToList());
    }
}
