
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Orders.Queries.GetOrderDetail;

public class GetOrderDetailQuery : IRamshaQuery
{
    public Guid OrderId { get; set; }
}
