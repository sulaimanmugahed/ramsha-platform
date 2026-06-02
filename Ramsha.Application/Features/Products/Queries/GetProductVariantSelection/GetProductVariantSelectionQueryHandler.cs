
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductVariantSelection;

public class GetProductVariantSelectionQueryHandler(
    IProductRepository productRepository
) : RamshaQueryHandler<GetProductVariantSelectionQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductVariantSelectionQuery request, CancellationToken cancellationToken)
    {
        return Success(await productRepository.GetProductVariantSelection(new Domain.Products.ProductId(request.ProductId), request.IsCatalog));
    }
}
