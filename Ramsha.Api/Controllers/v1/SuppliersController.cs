using Asp.Versioning;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Features.Suppliers.Commands.AddSupplyRequestItem;
using Ramsha.Application.Features.Suppliers.Commands.Create;
using Ramsha.Application.Features.Suppliers.Commands.CreateSupplyRequest;
using Ramsha.Application.Features.Suppliers.Commands.SendSupplyRequest;
using Ramsha.Application.Features.Suppliers.Queries.GetSupplyRequestsBySupplier;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierSupplyRequest;
using Ramsha.Application.Features.Suppliers.Commands.RemoveSupplyRequestItem;
using Ramsha.Application.Features.Suppliers.Queries.GetSupplyRequestItem;
using Ramsha.Application.Features.Suppliers.Queries.GetSupplierInventoryItems;
using Ramsha.Application.Features.Suppliers.Commands.AddSupplierVariant;
using Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProducts;
using Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProductVariant;
using Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProductVariantList;
using Ramsha.Application.Features.Suppliers.Commands.UpdateSupplierVariant;
using Ramsha.Application.Dtos.Supplies;

namespace Ramsha.Api.Controllers.v1;

/// <summary>
/// Manages supplier-related operations.
/// </summary>
[ApiVersion("1.0")]
[Authorize(Roles = Roles.Supplier)]
public class SuppliersController : BaseApiController
{
	/// <summary>
	/// Adds a variant to a supplier's product.
	/// </summary>
	/// <remarks>
	/// This endpoint allows suppliers to add a new variant to one of their products.
	/// </remarks>
	[HttpPost("products/{productId}/variants/{variantId}")]
	public async Task<ActionResult> CreateProduct(Guid productId, Guid variantId, AddSupplierVariantCommand command)
	{
		command.ProductId = productId;
		command.ProductVariantId = variantId;
		return await Command(command);
	}

	/// <summary>
	/// Retrieves a paged list of the current supplier's products.
	/// </summary>
	/// <remarks>
	/// This endpoint returns a paginated list of products associated with the currently authenticated supplier.
	/// </remarks>
	[HttpPost("products/paged")]
	public async Task<ActionResult<List<SupplierProductDto>>> GetCurrentSupplierProducts(GetCurrentSupplierProductsQuery query)
		=> await Query(query);

	/// <summary>
	/// Retrieves a specific variant of a supplier's product.
	/// </summary>
	/// <remarks>
	/// This endpoint returns the details of a specific variant for a product owned by the currently authenticated supplier.
	/// </remarks>
	[HttpGet("products/{productId}/variants/{variantId}")]
	public async Task<ActionResult<SupplierVariantDto?>> GetCurrentSupplierVariant(Guid productId, Guid variantId)
		=> await Query(new GetCurrentSupplierProductVariantQuery { ProductId = productId, VariantId = variantId });

	/// <summary>
	/// Retrieves all variants of a supplier's product.
	/// </summary>
	/// <remarks>
	/// This endpoint returns a list of all variants for a product owned by the currently authenticated supplier.
	/// </remarks>
	[HttpGet("products/{productId}/variants/")]
	public async Task<ActionResult<List<SupplierVariantDto>>> GetCurrentSupplierVariants(Guid productId)
		=> await Query(new GetCurrentSupplierProductVariantListQuery { ProductId = productId });

	/// <summary>
	/// Updates a variant of a supplier's product.
	/// </summary>
	/// <remarks>
	/// This endpoint updates the details of a specific variant for a product owned by the currently authenticated supplier.
	/// </remarks>
	[HttpPut("products/{productId}/variants/{variantId}")]
	public async Task<ActionResult> UpdateVariant(Guid productId, Guid variantId, UpdateSupplierVariantCommand command)
	{
		command.ProductId = productId;
		command.VariantId = variantId;
		return await Command(command);
	}

	/// <summary>
	/// Retrieves the current supplier's supply request.
	/// </summary>
	/// <remarks>
	/// This endpoint returns the details of the currently active supply request for the authenticated supplier.
	/// </remarks>
	[HttpGet("supply-request")]
	public async Task<ActionResult<SupplyRequestDto?>> SupplyRequest()
		=> await Query(new GetCurrentSupplierSupplyRequestQuery());

	/// <summary>
	/// Creates or register a new supplier.
	/// </summary>
	/// <remarks>
	/// This endpoint create or register a new supplier account.
	/// </remarks>
	[HttpPost, AllowAnonymous]
	public async Task<ActionResult<ActionResult<string>>> Create(CreateSupplierCommand command)
		=> await Command(command);

	/// <summary>
	/// Creates a new supply request.
	/// </summary>
	/// <remarks>
	/// This endpoint allows suppliers to create a new supply request.
	/// </remarks>
	[HttpPost("supply-request")]
	public async Task<ActionResult<string?>> CreateSupplyRequest(CreateSupplyRequestCommand command)
		=> await Command(command);

	/// <summary>
	/// Adds or updates an item in the current supply request.
	/// </summary>
	/// <remarks>
	/// This endpoint allows suppliers to add or update items in their current supply request.
	/// </remarks>
	[HttpPost("supply-request/items")]
	public async Task<ActionResult<SupplyRequestDto>> AddOrUpdateSupplyRequestItem(AddOrUpdateSupplyRequestItemCommand command)
		=> await Command(command);

	/// <summary>
	/// Retrieves a paged list of the current supplier's supplies.
	/// </summary>
	/// <remarks>
	/// This endpoint returns a paginated list of supplies associated with the currently authenticated supplier.
	/// </remarks>
	[HttpPost("supplies")]
	public async Task<ActionResult<List<SupplyDto>>> GetCurrentSupplierSupplies([FromBody] GetCurrentSupplierSuppliesQuery query)
		=> await Query(query);

	/// <summary>
	/// Removes an item from the current supply request.
	/// </summary>
	/// <remarks>
	/// This endpoint allows suppliers to remove an item from their current supply request.
	/// </remarks>
	[HttpDelete("supply-request/items/{id}")]
	public async Task<ActionResult> AddSupplyRequestItem(Guid id)
		=> await Command(new RemoveSupplyRequestItemCommand { SupplyRequestItemId = id });

	/// <summary>
	/// Retrieves details of a specific supply request item.
	/// </summary>
	/// <remarks>
	/// This endpoint returns the details of a specific item in the current supply request.
	/// </remarks>
	[HttpGet("supply-request/items/{id}")]
	public async Task<ActionResult> GetSupplyRequestItem(Guid id)
		=> await Query(new GetSupplyRequestItemQuery { SupplyRequestItemId = id });

	/// <summary>
	/// Sends the current supply request.
	/// </summary>
	/// <remarks>
	/// This endpoint allows suppliers to send their current supply request for processing.
	/// </remarks>
	[HttpPost("supply-request/send")]
	public async Task<ActionResult> SendSupplyRequest(SendSupplyRequestCommand command)
		=> await Command(command);

	/// <summary>
	/// Retrieves the current supplier's inventory items.
	/// </summary>
	/// <remarks>
	/// This endpoint returns a list of inventory items associated with the currently authenticated supplier.
	/// </remarks>
	[HttpGet("inventory/items")]
	public async Task<ActionResult> GetInventoryItems()
		=> await Query(new GetSupplierInventoryItemsQuery());
}