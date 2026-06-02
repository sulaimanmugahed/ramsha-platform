using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;

using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Helpers;
using Ramsha.Application.Services;
using Ramsha.Caching;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    ITagRepository tagRepository,
    IBrandRepository brandRepository,
    IVariantService variantService,
    IOptionRepository optionRepository,
    ICodeGenerator codeGenerator,
    IRamshaCache cacheService,
    IBackgroundJobService backgroundJobService
) : RamshaCommandHandler<CreateProductCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name, request.Description);

        var category = await categoryRepository.FindAsync(new Domain.Products.CategoryId(request.Category));
        if (category is null)
            return Invalid(message: "no category was found");

        product.SetCategory(category.Id);


        var brand = await brandRepository.FindAsync(new Domain.Products.BrandId(request.Brand));
        if (brand is null)
            return Invalid(message: "no brand was found");


        product.SetBrand(brand.Id);

        var productCode = codeGenerator.GenerateProductCode(category.Code, product.Id.Value);
        product.SetCode(productCode);

        if (!string.IsNullOrEmpty(request.ImageUrl))
            product.SetImage(request.ImageUrl);

        if (request.Tags.HasItems())
        {
            foreach (var tag in request.Tags)
            {
                var existTag = await tagRepository.FindAsync(x => x.Name.ToLower() == tag.ToLower());
                if (existTag is null)
                {
                    existTag = Tag.Create(tag);
                    await tagRepository.AddAsync(existTag, true);
                }
                product.AddTag(existTag);
            }
        }

        if (request.SeoSettings is not null)
        {
            product.SetSeoSettings(request.SeoSettings);
        }

        if (request.Options.HasItems())
        {
            foreach (var option in request.Options)
            {
                var optionToAdd = await optionRepository.FindAsync(o => o.Id == new Domain.Products.OptionId(option.Id));
                if (optionToAdd is null)
                    return Invalid(message: "invalid option");
                product.AddOption(optionToAdd, option.Priority);
            }

            var variant = request.DefaultVariant;

            if (variantService.IsVariantExists(product.Variants, variant.VariantValues))
                return Invalid(message: "this variant is already exist");

            var productVariant = ProductVariant.Create(product.Id,
                    new DimensionalWeight(variant.Length, variant.Width, variant.Height),
                    variant.Weight);

            if (!string.IsNullOrEmpty(request.ImageUrl))
                productVariant.SetImage(request.ImageUrl);


            List<string> optionValuesNames = [];

            foreach (var variantValue in variant.VariantValues)
            {
                var option = product.Options.SingleOrDefault(x => x.OptionId.Value == variantValue.Option)?.Option;
                if (option is null)
                {
                    if (option is null)
                        return Invalid(message: "invalid option");
                }

                var value = await optionRepository.GetValue(new Domain.Products.OptionValueId(variantValue.Value));
                if (value is null)
                    return Invalid(message: "invalid option value");

                optionValuesNames.Add(value.Name);
                productVariant.AddValue(option.Id, value.Id);
            }

            productVariant.SetCode(codeGenerator.GenerateVariantCode(product.Code, optionValuesNames));
            productVariant.SetDefault(true);


            product.AddVariant(productVariant);
        }


        await productRepository.AddAsync(product);


        backgroundJobService.Enqueue(() => cacheService.RemoveByTagAsync(CacheKeysHelper.ProductCacheKeys.Tag));

        return Success(product.Id.Value.ToString());
    }

}
