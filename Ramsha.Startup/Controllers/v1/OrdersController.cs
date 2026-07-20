using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Features.Orders.Commands.CreateOrder;
using Ramsha.Application.Features.Orders.Commands.DeliverFulfillmentRequest;
using Ramsha.Application.Features.Orders.Commands.ShipFulfillmentRequest;
using Ramsha.Application.Features.Orders.Queries.GetAllFulfillmentRequestsPaged;
using Ramsha.Application.Features.Orders.Queries.GetCurrentDeliveryAgentFulfillments;
using Ramsha.Application.Features.Orders.Queries.GetCurrentSupplierFulfillmentRequests;
using Ramsha.Application.Features.Orders.Queries.GetCustomerOrders;
using Ramsha.Application.Features.Orders.Queries.GetFulfillmentRequestDetail;
using Ramsha.Application.Features.Orders.Queries.GetOrderDetail;
using Ramsha.Application.Features.Orders.Queries.GetOrdersPaged;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Constants;

namespace Ramsha.Api.Controllers.v1;

/// <summary>
/// Manages order-related operations.
/// </summary>
[ApiVersion("1.0"), Authorize]
public class OrdersController : BaseApiController
{
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <remarks>
    /// This endpoint allows customers to create a new order.
    /// </remarks>
    [Authorize(Roles = Roles.Customer)]
    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateOrderCommand createOrderCommand)
        => await Command(createOrderCommand);

    /// <summary>
    /// Retrieves the current user's orders.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a list of orders for the currently authenticated user.
    /// </remarks>
    [HttpGet("my-orders")]
    public async Task<ActionResult<List<OrderDto>>> GetMyOrders()
        => await Query(new GetCustomerOrdersQuery());

    /// <summary>
    /// Retrieves a paged list of orders.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a paginated list of orders based on the provided query parameters.
    /// </remarks>
    [HttpPost("paged")]
    public async Task<ActionResult<List<OrderDto>>> GetPaged(GetOrdersPagedQuery query)
        => await Query(query);

    /// <summary>
    /// Retrieves details of a specific order.
    /// </summary>
    /// <remarks>
    /// This endpoint returns the details of an order identified by its unique ID.
    /// </remarks>
    [HttpGet("{orderId}/detail")]
    public async Task<ActionResult<OrderDetailDto?>> GetOrderDetail(Guid orderId)
        => await Query(new GetOrderDetailQuery { OrderId = orderId });

    /// <summary>
    /// Retrieves a paged list of fulfillment requests for the current supplier.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a paginated list of fulfillment requests for the currently authenticated supplier.
    /// </remarks>
    [Authorize(Roles = Roles.Supplier)]
    [HttpPost("supplier-fulfillment-requests/paged")]
    public async Task<ActionResult<List<FulfillmentRequestDto>>> GetSupplierFulfillments(GetCurrentSupplierFulfillmentRequestsQuery query)
        => await Query(query);

    /// <summary>
    /// Retrieves a paged list of fulfillment requests for the current delivery agent.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a paginated list of fulfillment requests for the currently authenticated delivery agent.
    /// </remarks>
    [Authorize(Roles = Roles.DeliveryAgent)]
    [HttpPost("deliveryAgent-fulfillment-requests/paged")]
    public async Task<ActionResult<List<FulfillmentRequestDto>>> GetDeliveryAgentFulfillments(GetCurrentDeliveryAgentFulfillmentsQuery query)
        => await Query(query);

    /// <summary>
    /// Retrieves a paged list of all fulfillment requests.
    /// </summary>
    /// <remarks>
    /// This endpoint returns a paginated list of all fulfillment requests.
    /// </remarks>
    [Authorize]
    [HttpPost("fulfillment-requests/paged")]
    public async Task<ActionResult<List<FulfillmentRequestDto>>> GetAllFulfillments(GetAllFulfillmentRequestsPagedQuery query)
        => await Query(query);

    /// <summary>
    /// Retrieves details of a specific fulfillment request.
    /// </summary>
    /// <remarks>
    /// This endpoint returns the details of a fulfillment request identified by its unique ID.
    /// </remarks>
    [HttpGet("fulfillment-requests/{id}/detail")]
    public async Task<ActionResult<FulfillmentRequestDetailDto?>> FulfillDetail(Guid id)
        => await Query(new GetFulfillmentRequestDetailQuery { Id = id });

    /// <summary>
    /// Ships a fulfillment request.
    /// </summary>
    /// <remarks>
    /// This endpoint allows a super admin to mark a fulfillment request as shipped and assign it to a delivery agent.
    /// </remarks>
    [Authorize(Roles = Roles.SuperAdmin)]
    [HttpPost("{orderId}/fulfillment-requests/{fulfillmentId}/ship")]
    public async Task<ActionResult> ShipRequest(Guid orderId, Guid fulfillmentId, [FromQuery] Guid deliveryAgentId)
        => await Command(new ShipFulfillmentRequestCommand { OrderId = orderId, FulfillmentRequestId = fulfillmentId, DeliveryAgentId = deliveryAgentId });

    /// <summary>
    /// Marks a fulfillment request as delivered.
    /// </summary>
    /// <remarks>
    /// This endpoint marks a fulfillment request as delivered.
    /// </remarks>
    [HttpPost("{orderId}/fulfillment-requests/{fulfillmentId}/deliver")]
    public async Task<ActionResult> DeliverRequest(Guid orderId, Guid fulfillmentId)
        => await Command(new DeliverFulfillmentRequestCommand { OrderId = orderId, FulfillmentRequestId = fulfillmentId });
}