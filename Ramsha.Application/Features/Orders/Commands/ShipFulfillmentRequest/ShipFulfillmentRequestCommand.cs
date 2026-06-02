
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Orders.Commands.ShipFulfillmentRequest;

public class ShipFulfillmentRequestCommand : IRamshaCommand
{
    public Guid OrderId { get; set; }
    public Guid FulfillmentRequestId { get; set; }
    public Guid DeliveryAgentId { get; set; }

}
