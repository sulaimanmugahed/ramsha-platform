
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProductVariantList;

public class GetCurrentSupplierProductVariantListQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }
}
