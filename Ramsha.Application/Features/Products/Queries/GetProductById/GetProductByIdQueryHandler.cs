
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    IProductRepository productRepository
) : RamshaQueryHandler<GetProductByIdQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindAsync(
        p => p.Id.Value == request.Id,
        [ x => x.Category,
         X => X.Inventories]
        );

        return Success(product?.AsDto());
    }
}
