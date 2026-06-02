
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Commands.DeliverFulfillmentRequest;

public class DeliverFulfillmentRequestCommandHandler(
    IOrderRepository orderRepository
) : RamshaCommandHandler<DeliverFulfillmentRequestCommand>
{
    public override async Task<IRamshaResult> HandleAsync(DeliverFulfillmentRequestCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindAsync(x => x.Id == new Domain.Orders.OrderId(request.OrderId)
       , [x => x.FulfillmentRequests]);

        if (order is null)
            return Invalid(message: "no order found");

        if (order is null)
            return Invalid(message: "no fulfillmentRequest found");

        order.DeliverFulfillmentRequest(new Domain.Orders.FulfillmentRequestId(request.FulfillmentRequestId));

        return Success();
    }
}
