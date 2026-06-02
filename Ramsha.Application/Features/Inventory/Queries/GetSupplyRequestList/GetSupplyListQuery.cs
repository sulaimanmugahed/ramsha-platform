
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Suppliers.Enums;

using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Inventory.Queries.GetSupplyRequestList;

public class GetSupplyListQuery : IRamshaQuery
{
    public SupplyStatus Status { get; set; }
}
