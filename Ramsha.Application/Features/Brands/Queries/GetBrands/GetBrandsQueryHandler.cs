using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Brands.Queries.GetBrands;

public class GetBrandsQueryHandler(
    IBrandRepository brandRepository
) : RamshaQueryHandler<GetBrandsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var result = await brandRepository.GetListAsync();
        return Success(result.Select(x => x.AsDto()).ToList());
    }
}