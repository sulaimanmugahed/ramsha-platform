
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Supplies.Queries.GetSupplyDetail;

public class GetSupplyDetailQuery : IRamshaQuery
{
    public Guid Id { get; set; }
}
