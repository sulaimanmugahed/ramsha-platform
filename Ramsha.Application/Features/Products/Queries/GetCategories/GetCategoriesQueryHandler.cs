using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Queries.GetCategories;

public class GetCategoriesQueryHandler(
    ICategoryRepository categoryRepository
) : RamshaQueryHandler<GetCategoriesQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await categoryRepository.GetMainCategories();

        return Success(result.Select(x => x.AsCategoryDto()).ToList());
    }
}
