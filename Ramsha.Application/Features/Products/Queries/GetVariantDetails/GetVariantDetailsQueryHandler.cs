
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetVariantDetails;

public class GetVariantDetailsQueryHandler(
    IProductRepository productRepository
) : RamshaQueryHandler<GetVariantDetailsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetVariantDetailsQuery request, CancellationToken cancellationToken)
    {
        var variant = await productRepository.GetVariantDetails(
            new Domain.Products.ProductId(request.ProductId),
            new Domain.Products.ProductVariantId(request.ProductVariantId)
        );
        if (variant is null)
            return Invalid(message: "EmptyData");

        return Success(variant.AsDetailsDto());
    }
}
