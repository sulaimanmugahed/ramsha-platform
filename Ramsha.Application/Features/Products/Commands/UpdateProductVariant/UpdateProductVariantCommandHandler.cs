using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.UpdateProductVariant;

public class UpdateProductVariantCommandHandler(
    IProductRepository productRepository,
    IOptionRepository optionRepository,
    IVariantService variantService
) : RamshaCommandHandler<UpdateProductVariantCommand>
{
    public override async Task<IRamshaResult> HandleAsync(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductWithVariantsAndOptions(new Domain.Products.ProductId(request.ProductId));
        if (product is null)
            return Invalid(message: "no product was found");

        var variant = product.Variants.SingleOrDefault(v => v.Id.Value == request.VariantId);
        if (variant is null)
            return Invalid(message: "no variant was found");


        if (request.VariantValuesToRemove.HasItems())
        {
            foreach (var variantValue in request.VariantValuesToRemove)
            {
                variant.RemoveValue(new Domain.Products.OptionId(variantValue.Option)
                , new Domain.Products.OptionValueId(variantValue.Value));
            }
        }


        if (request.VariantValuesToAdd.HasItems())
        {
            if (variantService.IsVariantExists(product.Variants, request.VariantValuesToAdd))
                return Invalid(message: "this variant is already exist");

            foreach (var variantValue in request.VariantValuesToAdd)
            {
                var option = product.Options.SingleOrDefault(x => x.OptionId.Value == variantValue.Option)?.Option;
                option ??= await optionRepository.FindAsync(o => o.Id == new Domain.Products.OptionId(variantValue.Option),
                [o => o.OptionValues]);

                var value = option?.OptionValues.SingleOrDefault(v => v.Id.Value == variantValue.Value);
                if (option is null || value is null)
                    return Invalid(message: "option or option value is null");

                variant.AddValue(option.Id, value.Id);
            }
        }

        product.Update();

        return Success();
    }
}
