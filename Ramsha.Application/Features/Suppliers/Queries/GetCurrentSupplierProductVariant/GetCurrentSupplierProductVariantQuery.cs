
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProductVariant;

public class GetCurrentSupplierProductVariantQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }

}
