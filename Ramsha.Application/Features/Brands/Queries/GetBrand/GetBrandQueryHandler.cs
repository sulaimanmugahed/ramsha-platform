using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Brands.Queries.GetBrand;

public class GetBrandQueryHandler(
    IBrandRepository brandRepository
) : RamshaQueryHandler<GetBrandQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetBrandQuery request, CancellationToken cancellationToken)
    {
        var existBrand = await brandRepository.FindAsync(new Domain.Products.BrandId(request.Id));

        if (existBrand is null)
            return NotFound(message: "no brand with this id");
        return Success(existBrand.AsDto());
    }
}
