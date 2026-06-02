
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Queries.GetProductVariantSelection;

public class GetProductVariantSelectionQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }
    public bool IsCatalog { get; set; }
}
