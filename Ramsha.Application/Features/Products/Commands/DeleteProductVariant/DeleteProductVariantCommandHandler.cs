using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;


namespace Ramsha.Application.Features.Products.Commands.DeleteProductVariant;

public class DeleteProductVariantCommandHandler(
    IProductRepository productRepository
) : RamshaCommandHandler<DeleteProductVariantCommand>
{
    public override async Task<IRamshaResult> HandleAsync(DeleteProductVariantCommand request, CancellationToken cancellationToken)
    {
        var productId = new Domain.Products.ProductId(request.ProductId);
        var product = await productRepository.FindAsync(x => x.Id == productId, [x => x.Variants]);
        if (product is null)
            return Invalid(message: "no product was found");

        var variant = product.Variants.FirstOrDefault(
            x => x.Id == new Domain.Products.ProductVariantId(request.VariantId)
        );

        if (variant is null)
            return Invalid(message: "no product was found");

        product.Variants.Remove(variant);

        product.Update();

        return Success();
    }
}
