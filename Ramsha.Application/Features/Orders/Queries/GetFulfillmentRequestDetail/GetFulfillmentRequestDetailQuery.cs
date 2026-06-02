
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Orders.Queries.GetFulfillmentRequestDetail;

public class GetFulfillmentRequestDetailQuery : IRamshaQuery
{
    public Guid Id { get; set; }
}
