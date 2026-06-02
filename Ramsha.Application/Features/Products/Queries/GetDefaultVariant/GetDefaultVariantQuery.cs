
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Queries.GetDefaultVariant;

public class GetDefaultVariantQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }
}
