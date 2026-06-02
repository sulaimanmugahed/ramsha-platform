
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler(
        IBrandRepository brandRepository
) : RamshaCommandHandler<DeleteBrandCommand>
{
    public override async Task<IRamshaResult> HandleAsync(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var existBrand = await brandRepository.FindAsync(new Domain.Products.BrandId(request.Id));
        if (existBrand is null)
            return NotFound(message: "no brand with this id");

        await brandRepository.DeleteAsync(existBrand);

        return Success();
    }
}
