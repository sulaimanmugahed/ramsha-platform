
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.UpdateProductVariant;

public class ChangeProductStatusCommandHandler(
IProductRepository productRepository
) : RamshaCommandHandler<ChangeProductStatusCommand>
{
    public override async Task<IRamshaResult> HandleAsync(ChangeProductStatusCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindAsync(new Domain.Products.ProductId(request.ProductId));
        if (product is null)
            return Invalid(message: "no product was found");

        product.SetStatus(request.Status);
        product.Update();

        return Success();
    }
}
