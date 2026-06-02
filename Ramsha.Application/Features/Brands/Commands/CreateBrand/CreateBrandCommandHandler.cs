using System;
using System.Collections.Generic;

using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler(
    IBrandRepository brandRepository
) : RamshaCommandHandler<CreateBrandCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = Brand.Create(request.Name);

        await brandRepository.AddAsync(brand);

        return Success(brand.Id.Value.ToString());
    }
}
