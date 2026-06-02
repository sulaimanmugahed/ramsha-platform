
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommandHandler(
    IBrandRepository brandRepository
) : RamshaCommandHandler<UpdateBrandCommand>
{
    public override async Task<IRamshaResult> HandleAsync(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var existBrand = await brandRepository.FindAsync(new Domain.Products.BrandId(request.Id));
        if (existBrand is null)
            return NotFound(message: "no brand with this id");

        existBrand.Update(request.Name);

        return Success();
    }
}
