using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetDefaultVariant;

public class GetDefaultVariantQueryHandler(IProductRepository productRepository)
: RamshaQueryHandler<GetDefaultVariantQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetDefaultVariantQuery request, CancellationToken cancellationToken)
    {
        var variant = await productRepository.GetDefaultVariant(new Domain.Products.ProductId(request.ProductId));
        return Success(variant?.AsDto());
    }
}
