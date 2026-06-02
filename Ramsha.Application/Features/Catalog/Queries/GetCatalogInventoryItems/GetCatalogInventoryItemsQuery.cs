
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogInventoryItems;

public class GetCatalogInventoryItemsQuery : PagedParams, IRamshaQuery
{
    public Guid ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }

}
