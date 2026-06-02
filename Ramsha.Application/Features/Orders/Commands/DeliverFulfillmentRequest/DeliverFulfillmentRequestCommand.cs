
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Orders.Commands.DeliverFulfillmentRequest;

public class DeliverFulfillmentRequestCommand : IRamshaCommand
{
    public Guid FulfillmentRequestId { get; set; }
    public Guid OrderId { get; set; }


}
