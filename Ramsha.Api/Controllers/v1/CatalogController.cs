using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Ramsha.Application.Dtos.Catalog;
using Ramsha.Application.Features.Catalog.Queries.GetCatalogCategories;
using Ramsha.Application.Features.Catalog.Queries.GetCatalogInventoryItems;
using Ramsha.Application.Features.Catalog.Queries.GetCatalogProductDetail;
using Ramsha.Application.Wrappers;
using Ramsha.Application.Features.Catalog.Queries.GetCatalogProductVariant;
using Ramsha.Application.Features.Catalog.Queries.GetCatalogProductsPaged;

namespace Ramsha.Api.Controllers.v1;

/// <summary>
/// Manages catalog-related operations.
/// </summary>
[ApiVersion("1.0")]
public class CatalogController : BaseApiController
{
    /// <summary>
    /// Retrieves a paged list of catalog products.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a paginated list of products available in the catalog based on the provided query parameters.
    /// </remarks>
    [HttpPost("products")]
    public async Task<ActionResult<List<CatalogProductDto>>> GetProductsPaged([FromBody] GetCatalogProductsPagedQuery query)
        => await Query(query);

    /// <summary>
    /// Retrieves detailed information about a catalog product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns detailed information about a product in the catalog identified by its unique ID.
    /// </remarks>
    [HttpGet("products/{productId}")]
    public async Task<ActionResult<CatalogProductDetailDto>> GetProductCatalogDetail(Guid productId)
        => await Query(new GetCatalogProductDetailQuery { ProductId = productId });

    /// <summary>
    /// Retrieves details of a specific catalog product variant.
    /// </summary>
    /// <remarks>
    /// This endpoint returns details of a specific variant for a product in the catalog. If no variant ID is provided, it returns the default variant.
    /// </remarks>
    [HttpGet("products/{productId}/variants/{variantId?}")]
    public async Task<ActionResult<CatalogVariantDto?>> GetCatalogProductVariant(Guid productId, Guid? variantId = null)
        => await Query(new GetCatalogProductVariantQuery { ProductId = productId, ProductVariantId = variantId });

    /// <summary>
    /// Retrieves a list of catalog categories.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a list of all categories available in the catalog.
    /// </remarks>
    [HttpGet("categories")]
    public async Task<ActionResult<List<CatalogCategoryDto>>> GetCatalogCategories()
        => await Query(new GetCatalogCategoriesQuery());

    /// <summary>
    /// Retrieves inventory items for a catalog product.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a list of inventory items for a product in the catalog. If a variant ID is provided, it filters the results for that specific variant.
    /// </remarks>
    [HttpPost("{productId}/inventoryItems/{productVariantId?}")]
    public async Task<ActionResult<List<CatalogInventoryItemDetailDto>>> GetInventoryItems(Guid productId, [FromBody] GetCatalogInventoryItemsQuery query, [FromRoute] Guid? productVariantId = null)
    {
        query.ProductId = productId;
        query.ProductVariantId = productVariantId;
        return await Query(query);
    }
}