
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.DeleteProductOption;

public class DeleteProductOptionCommandHandler(
    IProductRepository productRepository

) : RamshaCommandHandler<DeleteProductOptionCommand>
{
    public override async Task<IRamshaResult> HandleAsync(DeleteProductOptionCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductWithOptions(new Domain.Products.ProductId(request.ProductId));
        if (product is null)
            return Invalid(message: "no product was found");

        var option = product.Options.FirstOrDefault(o => o.OptionId.Value == request.OptionId);
        if (product is null)
            return Invalid(message: "no option was found");

        product.Options.Remove(option!);

        product.Update();


        return Success();
    }
}
