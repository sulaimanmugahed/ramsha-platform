

using Ramsha.Domain.Products;

namespace Ramsha.Domain.Inventory.Events;

public record StockDecreased(
  ProductId ProductId,
ProductVariantId? ProductVariantId
)
{ }
