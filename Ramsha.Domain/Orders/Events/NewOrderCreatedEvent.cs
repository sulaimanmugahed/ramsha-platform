using Ramsha.Domain.Orders.Entities;

namespace Ramsha.Domain.Orders.Events;

public record NewOrderCreatedEvent(
    OrderId OrderId,
    ShippingAddress ShippingAddress
)
{ };


