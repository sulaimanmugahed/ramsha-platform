
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.Application.Contracts.Caching;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Extensions;
using Ramsha.Application.Services;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository
) : RamshaCommandHandler<UpdateProductCommand>
{
    public override async Task<IRamshaResult> HandleAsync(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToEdit = await productRepository.GetProductDetails(new Domain.Products.ProductId(request.ProductId));
        if (productToEdit is null)
            return Invalid(message: "no product was found");

        var basicData = request.ProductBasicCommand;
        var additionalData = request.ProductAdditionalCommand;

        if (basicData is not null)
        {
            productToEdit.SetName(basicData.Name);
            productToEdit.SetDescription(basicData.Description);

            if (basicData.Brand.HasValue)
            {
                var brand = await brandRepository.FindAsync(new Domain.Products.BrandId(basicData.Brand.Value));
                if (brand is null)
                    return Invalid(message: "no brand was found");
                productToEdit.SetBrand(brand.Id);
            }

            if (basicData.Category.HasValue)
            {
                var category = await categoryRepository.FindAsync(new Domain.Products.CategoryId(basicData.Category.Value));
                if (category is null)
                    return Invalid(message: "no category was found");
                productToEdit.SetCategory(category.Id);
            }

            if (basicData.ImageUrl is not null)
                productToEdit.SetImage(basicData.ImageUrl);

        }


        if (additionalData is not null)
        {
            if (additionalData.SeoSettings is not null)
                productToEdit.SetSeoSettings(additionalData.SeoSettings);

            if (additionalData.TagsToAdd.HasItems())
            {
                foreach (var tag in additionalData.TagsToAdd!)
                {
                    var existTag = await tagRepository.FindAsync(x => x.Name.ToLower() == tag.ToLower());
                    if (existTag is null)
                    {
                        existTag = Tag.Create(tag);
                        await tagRepository.AddAsync(existTag, true);
                    }
                    productToEdit.AddTag(existTag);
                }
            }

            if (additionalData.TagsToRemove.HasItems())
            {
                productToEdit.RemoveTags(additionalData.TagsToRemove!);
            }
        }
        productToEdit.Update();

        return Success();
    }
}
