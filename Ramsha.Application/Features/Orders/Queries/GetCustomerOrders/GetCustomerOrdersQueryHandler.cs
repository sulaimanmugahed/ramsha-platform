
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Orders;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Queries.GetCustomerOrders;

public class GetCustomerOrdersQueryHandler(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IAuthenticatedUserService authenticatedUser

) : RamshaQueryHandler<GetCustomerOrdersQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.FindByUsername(authenticatedUser.UserName);
        if (customer is null)
            return Unauthenticated();


        var orders = await orderRepository.GetListAsync(x => x.CustomerId == customer.Id);
        return Success(orders.Select(o => o.AsDto()).ToList());
    }
}
