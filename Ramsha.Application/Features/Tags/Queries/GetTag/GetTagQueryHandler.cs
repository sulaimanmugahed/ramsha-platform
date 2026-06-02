using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Tags.Queries.GetTag;

public class GetTagQueryHandler(
    ITagRepository tagRepository
) : RamshaQueryHandler<GetTagQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetTagQuery request, CancellationToken cancellationToken)
    {
        var existTag = await tagRepository.FindAsync(new TagId(request.Id));
        if (existTag is null)
            return NotFound(message: "no tag with this id");


        return Success(existTag.AsDto());
    }
}
