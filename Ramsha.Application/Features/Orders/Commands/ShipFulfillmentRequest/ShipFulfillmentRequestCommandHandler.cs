

using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Commands.ShipFulfillmentRequest;

public class ShipFulfillmentRequestCommandHandler(
    IOrderRepository orderRepository
) : RamshaCommandHandler<ShipFulfillmentRequestCommand>
{
    public override async Task<IRamshaResult> HandleAsync(ShipFulfillmentRequestCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindAsync(x => x.Id == new Domain.Orders.OrderId(request.OrderId)
        , [x => x.FulfillmentRequests]);

        if (order is null)
            return Invalid(message: "no order found");

        if (order is null)
            return Invalid(message: "no fulfillmentRequest found");

        order.ShipFulfillmentRequest(
            new Domain.Orders.FulfillmentRequestId(request.FulfillmentRequestId),
            new Domain.DeliveryAgents.DeliveryAgentId(request.DeliveryAgentId));

        return Success();
    }
}
