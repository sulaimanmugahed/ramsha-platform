
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Queries.GetSupplyRequestItem;

public class GetSupplyRequestItemQuery : IRamshaQuery
{
    public Guid SupplyRequestItemId { get; set; }
}
