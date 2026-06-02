using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Tags.Queries.GetTags;

public class GetTagsQueryHandler(
    ITagRepository tagRepository
) : RamshaQueryHandler<GetTagsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var result = await tagRepository.GetListAsync();
        return Success(result.Select(t => t.AsDto()).ToList());
    }
}
