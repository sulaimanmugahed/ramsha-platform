
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogProductVariant;

public class GetCatalogProductVariantQuery : IRamshaQuery
{
    public Guid? ProductVariantId { get; set; }
    public Guid ProductId { get; set; }
}
