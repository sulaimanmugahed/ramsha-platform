
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductsDetails;

public class GetProductsDetailsQueryHandler(
    IProductRepository productRepository)
 : RamshaQueryHandler<GetProductDetailsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetProductDetails(new Domain.Products.ProductId(request.ProductId));
        return Success(products?.AsDetailsDto());
    }
}
