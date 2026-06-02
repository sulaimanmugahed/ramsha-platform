
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Queries.GetOrderDetail;

public class GetOrderDetailQueryHandler(
    IOrderRepository orderRepository
) : RamshaQueryHandler<GetOrderDetailQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.FindAsync(
            x => x.Id == new Domain.Orders.OrderId(request.OrderId),
           [x => x.ShippingAddress, X => X.FulfillmentRequests]);

        return Success(order?.AsDetailDto());
    }
}
