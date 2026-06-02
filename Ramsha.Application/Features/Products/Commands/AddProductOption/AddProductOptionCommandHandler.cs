
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.AddProductOption;

public class AddProductOptionCommandHandler(
    IProductRepository productRepository,
    IOptionRepository optionRepository
) : RamshaCommandHandler<AddProductOptionCommand>
{
    public override async Task<IRamshaResult> HandleAsync(AddProductOptionCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindAsync(new Domain.Products.ProductId(request.ProductId));
        if (product is null)
            return Invalid(message: "no product was found");

        var option = await optionRepository.FindAsync(new Domain.Products.OptionId(request.OptionId));
        if (option is null)
            return Invalid(message: "no option was found");

        product.AddOption(option, request.Priority);
        product.Update();

        return Success();
    }
}
