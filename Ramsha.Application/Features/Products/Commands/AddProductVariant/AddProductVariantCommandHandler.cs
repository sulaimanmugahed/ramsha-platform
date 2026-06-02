
using System.Text;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;

using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Services;
using Ramsha.Application.Contracts.Caching;
using Ramsha.Application.Helpers;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.AddProductVariant;

public class AddProductVariantCommandHandler(
    IProductRepository productRepository,
    IOptionRepository optionRepository,
    IVariantService variantService,
    ICodeGenerator codeGenerator,
    IBackgroundJobService backgroundJobService,
   IRamshaCache cacheService

) : RamshaCommandHandler<AddProductVariantCommand>
{
    public override async Task<IRamshaResult> HandleAsync(AddProductVariantCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductWithVariantsAndOptions(new Domain.Products.ProductId(request.ProductId));
        if (product is null)
            return Invalid(message: "no product was found");


        if (variantService.IsVariantExists(product.Variants, request.VariantValues))
            return Invalid(message: "this variant is already exist");

        var productVariant = ProductVariant.Create(product.Id,
                new DimensionalWeight(request.Length, request.Width, request.Height),
                request.Weight);

        if (!string.IsNullOrEmpty(request.ImageUrl))
            productVariant.SetImage(request.ImageUrl);


        List<string> optionValuesNames = [];

        foreach (var variantValue in request.VariantValues)
        {
            var option = product.Options.SingleOrDefault(x => x.OptionId.Value == variantValue.Option)?.Option;
            if (option is null)
            {
                option = await optionRepository.FindAsync(o => o.Id == new Domain.Products.OptionId(variantValue.Option), [o => o.OptionValues]);
                if (option is null)
                    return Invalid(message: "invalid option");
                product.AddOption(option);
            }

            var value = option?.OptionValues.SingleOrDefault(v => v.Id.Value == variantValue.Value);
            if (value is null)
                return Invalid(message: "invalid option value");

            optionValuesNames.Add(value.Name);
            productVariant.AddValue(option.Id, value.Id);
        }

        productVariant.SetCode(codeGenerator.GenerateVariantCode(product.Code, optionValuesNames));


        product.AddVariant(productVariant);

        product.Update();


        backgroundJobService.Enqueue(() => cacheService.RemoveByTagAsync(new string[] { CacheKeysHelper.ProductCacheKeys.Tag, CacheKeysHelper.ProductCacheKeys.Tag + request.ProductId }));

        return Success();
    }
}
