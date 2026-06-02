using Asp.Versioning;
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Features.Products.Commands.AddProductOption;
using Ramsha.Application.Features.Products.Commands.AddProductVariant;
using Ramsha.Application.Features.Products.Commands.CreateProduct;
using Ramsha.Application.Features.Products.Commands.DeleteProductOption;
using Ramsha.Application.Features.Products.Commands.DeleteProductRange;
using Ramsha.Application.Features.Products.Commands.DeleteProductVariant;
using Ramsha.Application.Features.Products.Commands.UpdateProductVariant;
using Ramsha.Application.Features.Products.Queries.GetProductById;
using Ramsha.Application.Features.Products.Queries.GetProductOptions;
using Ramsha.Application.Features.Products.Queries.GetProductsDetails;
using Ramsha.Application.Features.Products.Queries.GetProductsPaged;
using Ramsha.Application.Features.Products.Queries.GetProductVariants;
using Ramsha.Application.Features.Products.Queries.GetVariantDetails;
using Ramsha.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Ramsha.Domain.Products.Enums;
using Ramsha.Application.Features.Products.Commands.UpdateProduct;
using Ramsha.Application.Features.Products.Queries.GetProductVariantSelection;
using Ramsha.Application.Features.Products.Queries.GetDefaultVariant;
using Ramsha.Application.Features.Products.Commands.DeleteProduct;

namespace Ramsha.Api.Controllers.v1;

/// <summary>
/// Manages product-related operations.
/// </summary>
[ApiVersion("1.0")]
public class ProductsController : BaseApiController
{
    /// <summary>
    /// Retrieves a paged list of products.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a paginated list of products based on the provided query parameters.
    /// </remarks>
    [HttpPost("paged")]
    public async Task<ActionResult<List<ProductDto>>> GetProductsPaged([FromBody] GetProductsPagedQuery query)
        => await Query(query);

    /// <summary>
    /// Retrieves variant selection details for a product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns variant selection details for a product such as available options, and variants data, useful for some pages.
    /// </remarks>
    [HttpGet("{productId}/selection")]
    public async Task<ActionResult<ProductVariantSelectionDto?>> GetProductVariantSelection(Guid productId, bool isCatalog)
        => await Query(new GetProductVariantSelectionQuery { ProductId = productId, IsCatalog = isCatalog });

    /// <summary>
    /// Retrieves options for a specific product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a list of options available for a product identified by its ID.
    /// </remarks>
    [HttpGet("{productId}/options")]
    public async Task<ActionResult<List<ProductOptionDto>?>> GetProductOption(Guid productId)
        => await Query(new GetProductOptionsQuery { ProductId = productId });

    /// <summary>
    /// Retrieves a specific product by its ID.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a product identified by its unique ID.
    /// </remarks>
    [HttpGet("{productId}", Name = nameof(GetProduct))]
    public async Task<ActionResult<ProductDto?>> GetProduct(Guid productId)
        => await Query(new GetProductByIdQuery { Id = productId });

    /// <summary>
    /// Retrieves detailed information about a product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns comprehensive details about a product, including variants and options.
    /// </remarks>
    [HttpGet("{productId}/detail", Name = nameof(GetProductDetail))]
    public async Task<ActionResult<ProductDetailsDto?>> GetProductDetail(Guid productId)
        => await Query(new GetProductDetailsQuery { ProductId = productId });

    /// <summary>
    /// Deletes a range of products.
    /// </summary>
    /// <remarks>
    /// This endpoint deletes multiple products based on the provided IDs.
    /// </remarks>
    [HttpDelete]
    public async Task<ActionResult> RemoveRange(DeleteProductRangeCommand command)
        => await Command(command);

    /// <summary>
    /// Delete a specific product by its ID.
    /// </summary>
    /// <remarks>
    /// This endpoint delete a product that match ID provided.
    /// </remarks>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(Guid id)
        => await Command(new DeleteProductCommand { Id = id });

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <remarks>
    /// This endpoint creates a new product with the provided details and returns the ID of the created product.
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<ActionResult<string?>>> CreateProducts(CreateProductCommand command)
    {
        return await Command(command);
    }

    /// <summary>
    /// Adds an option to a product.
    /// </summary>
    /// <remarks>
    /// This endpoint adds an option to a product identified by its ID.
    /// </remarks>
    [HttpPost("{productId}/options/{optionId}")]
    public async Task<ActionResult> AddOption(Guid productId, Guid optionId)
        => await Command(new AddProductOptionCommand { ProductId = productId, OptionId = optionId });

    /// <summary>
    /// Removes an option from a product.
    /// </summary>
    /// <remarks>
    /// This endpoint removes an option from a product identified by its ID.
    /// </remarks>
    [HttpDelete("{productId}/options/{optionId}")]
    public async Task<ActionResult> RemoveOption(Guid productId, Guid optionId)
        => await Command(new DeleteProductOptionCommand { ProductId = productId, OptionId = optionId });

    /// <summary>
    /// Adds a variant to a product.
    /// </summary>
    /// <remarks>
    /// This endpoint adds a new variant to a product identified by its ID.
    /// </remarks>
    [HttpPost("{productId}/variants")]
    public async Task<ActionResult> AddProductVariant(Guid productId, AddProductVariantCommand command)
    {
        command.ProductId = productId;
        return await Command(command);
    }

    /// <summary>
    /// Retrieves the default variant for a product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns the default variant for a product identified by its ID.
    /// </remarks>
    [HttpGet("{productId}/variants/default")]
    public async Task<ActionResult<ProductVariantDto?>> GetDefaultVariant(Guid productId)
        => await Query(new GetDefaultVariantQuery { ProductId = productId });

    /// <summary>
    /// Retrieves all variants for a product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a list of all variants for a product identified by its ID.
    /// </remarks>
    [HttpGet("{productId}/variants")]
    public async Task<ActionResult<List<ProductVariantDto?>>> GetVariantsForProduct(Guid productId)
        => await Query(new GetProductVariantsQuery { ProductId = productId });

    /// <summary>
    /// Retrieves details of a specific variant.
    /// </summary>
    /// <remarks>
    /// This endpoint returns the details of a variant identified by its ID and product ID.
    /// </remarks>
    [HttpGet("{productId}/variants/{variantId}")]
    public async Task<ActionResult<ProductVariantDto>> GetVariant(Guid productId, Guid variantId)
        => await Query(new GetProductVariantQuery { ProductId = productId, VariantId = variantId });

    /// <summary>
    /// Deletes a variant from a product.
    /// </summary>
    /// <remarks>
    /// This endpoint deletes a variant identified by its ID and product ID.
    /// </remarks>
    [HttpDelete("{productId}/variants/{variantId}")]
    public async Task<ActionResult> DeleteVariant(Guid productId, Guid variantId)
        => await Command(new DeleteProductVariantCommand { ProductId = productId, VariantId = variantId });

    /// <summary>
    /// Updates a product.
    /// </summary>
    /// <remarks>
    /// This endpoint updates the details of a product identified by its ID.
    /// </remarks>
    [HttpPut("{productId}")]
    public async Task<ActionResult> UpdateProduct(Guid productId, UpdateProductCommand command)
    {
        command.ProductId = productId;
        return await Command(command);
    }

    /// <summary>
    /// Updates a variant of a product.
    /// </summary>
    /// <remarks>
    /// This endpoint updates the details of a variant identified by its ID and product ID.
    /// </remarks>
    [HttpPut("{productId}/variants/{variantId}")]
    public async Task<ActionResult> UpdateVariant(Guid productId, Guid variantId, UpdateProductVariantCommand command)
    {
        command.VariantId = variantId;
        command.ProductId = productId;
        return await Command(command);
    }

    /// <summary>
    /// Changes the status of a product.
    /// </summary>
    /// <remarks>
    /// This endpoint updates the status of a product identified by its ID.
    /// </remarks>
    [HttpPut("{productId}/status")]
    public async Task<ActionResult> ChangeProductStatus([FromRoute] Guid productId, [FromQuery] ProductStatus statusValue)
        => await Command(new ChangeProductStatusCommand { ProductId = productId, Status = statusValue });
}