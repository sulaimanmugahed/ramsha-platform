
using Ramsha.LocalMessaging.Abstractions;


namespace Ramsha.Application.Features.Catalog.Queries.GetCatalogProductDetail;

public class GetCatalogProductDetailQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }
}
