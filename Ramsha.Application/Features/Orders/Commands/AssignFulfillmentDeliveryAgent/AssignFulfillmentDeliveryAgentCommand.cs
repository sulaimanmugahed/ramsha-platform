
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Orders.Commands.AssignFulfillmentDeliveryAgent;

public class AssignFulfillmentDeliveryAgentCommand : IRamshaCommand
{
    public Guid FulfillmentRequestId { get; set; }
    public Guid DeliveryAgentId { get; set; }

}
